using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using _3rd.Excel.Editor;
using ExcelDataReader;
using I2.Loc;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Excel.Editor
{
    /// <summary>
    /// 解析Excel
    /// </summary>
    public class EditorParseExcel
    {
        private static float _rate;
        private static string _describe = "开始解析";
        private static IExcelDataReader excelDataReader;
        private static FileStream stream;

        [MenuItem("Tools/ReadExcel-Specific")]
        private static void ReadExcelSpecific()
        {
            ExcelSpecificWindow.ShowWindow();
        }

        public static void ReadGameData(List<string> tables)
        {
            _rate = 0;
            _describe = "开始解析";
            EditorCoroutineRunner.StartEditorCoroutine(ReadExcel(tables));
            EditorApplication.update += Update;
        }

        [MenuItem("Tools/ReadExcel")]
        private static void ReadGameData()
        {
            var tables = ExcelToAssets.tables;
            ReadGameData(tables);
        }

        /// <summary>
        /// 解析Excel
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        private static IEnumerator ReadExcel(List<string> tables)
        {
            var filePath = Application.dataPath + ExcelToAssets.SourceFile;
            stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            yield return AsDataSet(excelDataReader, tables, ExcelToAssets.CreatAsset);
            _rate = 1.1f;
            excelDataReader.Close();
        }

        /// <summary>
        /// 解析DataSet
        /// </summary>
        /// <param name="self"></param>
        /// <param name="tables"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private static IEnumerator AsDataSet(IExcelDataReader self, List<string> tables, Func<DataSet, List<string>, IEnumerator> action)
        {
            self.Reset();
            int num = -1;
            DataSet dataset = new DataSet();
            do
            {
                ++num;
                var configuration1 = new ExcelDataTableConfiguration();
                yield return AsDataTable(self, tables, configuration1, table => { dataset.Tables.Add(table); });
            } while (self.NextResult());

            dataset.AcceptChanges();

            FixDataTypes(dataset);
            self.Reset();
            yield return action.Invoke(dataset, tables);
        }

        /// <summary>
        /// 解析DataTable
        /// </summary>
        /// <param name="self"></param>
        /// <param name="tables"></param>
        /// <param name="configuration"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private static IEnumerator AsDataTable(IExcelDataReader self,
            List<string> tables,
            ExcelDataTableConfiguration configuration,
            Action<DataTable> action)
        {
            if (!tables.Contains(self.Name))
            {
                yield break;
            }

            var table = new DataTable
            {
                TableName = self.Name
            };

            table.ExtendedProperties.Add("visiblestate", self.VisibleState);
            var flag = true;
            var rowIndex = 0;
            var intList = new List<int>();
            var selfMergeCells = self.MergeCells;
            while (self.Read())
            {
                if (flag)
                {
                    if (configuration.UseHeaderRow && configuration.ReadHeaderRow != null)
                        configuration.ReadHeaderRow(self);
                    for (var i = 0; i < self.FieldCount; ++i)
                    {
                        if (configuration.FilterColumn == null || configuration.FilterColumn(self, i))
                        {
                            var name = configuration.UseHeaderRow ? Convert.ToString(self.GetValue(i)) : null;
                            if (string.IsNullOrEmpty(name))
                                name = configuration.EmptyColumnNamePrefix + i;
                            var column = new DataColumn(GetUniqueColumnName(table, name), typeof(object))
                            {
                                Caption = name
                            };
                            table.Columns.Add(column);
                            intList.Add(i);
                            yield return i;
                        }
                    }

                    table.BeginLoadData();
                    flag = false;
                    if (configuration.UseHeaderRow)
                        continue;
                }

                rowIndex++;

                if (configuration.FilterRow == null || configuration.FilterRow(self))
                {
                    if (IsEmptyRow(self)) continue;
                    var row = table.NewRow();
                    table.Rows.Add(row);
                    for (var index = 0; index < intList.Count; ++index)
                    {
                        var i = intList[index];
                        var obj = self.GetValue(i);
                        if (obj == null && selfMergeCells != null)
                        {
                            foreach (var selfMergeCell in selfMergeCells)
                            {
                                if (i >= selfMergeCell.FromColumn && rowIndex - 1 >= selfMergeCell.FromRow
                                    && i <= selfMergeCell.ToColumn  && rowIndex - 1 <= selfMergeCell.ToRow)
                                {
                                    var offset = rowIndex - table.Rows.Count;
                                    obj = table.Rows[selfMergeCell.FromRow - offset][selfMergeCell.FromColumn];
                                    break;
                                }
                            }
                        }
                        row[index] = obj;
                        yield return i;
                    }
                }
                _rate = rowIndex / (float) self.RowCount;
                _describe = table.TableName + "(" + rowIndex + "/" + self.RowCount + ")";
            }
            table.EndLoadData();
            action.Invoke(table);
        }


        private static string GetUniqueColumnName(DataTable table, string name)
        {
            string name1 = name;
            int num = 1;
            while (table.Columns[name1] != null)
            {
                name1 = $"{(object) name}_{(object) num}";
                ++num;
            }

            return name1;
        }

        private static void FixDataTypes(DataSet dataset)
        {
            List<DataTable> dataTableList = new List<DataTable>(dataset.Tables.Count);
            bool flag = false;
            foreach (DataTable table in dataset.Tables)
            {
                if (table.Rows.Count == 0)
                {
                    dataTableList.Add(table);
                }
                else
                {
                    DataTable dataTable = null;
                    for (int index = 0; index < table.Columns.Count; ++index)
                    {
                        Type type1 = null;
                        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
                        {
                            if (!row.IsNull(index))
                            {
                                Type type2 = row[index].GetType();
                                if ((object) type2 != (object) type1)
                                {
                                    if ((object) type1 == null)
                                    {
                                        type1 = type2;
                                    }
                                    else
                                    {
                                        type1 = (Type) null;
                                        break;
                                    }
                                }
                            }
                        }

                        if ((object) type1 != null)
                        {
                            flag = true;
                            if (dataTable == null)
                                dataTable = table.Clone();
                            dataTable.Columns[index].DataType = type1;
                        }
                    }

                    if (dataTable != null)
                    {
                        dataTable.BeginLoadData();
                        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
                            dataTable.ImportRow(row);
                        dataTable.EndLoadData();
                        dataTableList.Add(dataTable);
                    }
                    else
                        dataTableList.Add(table);
                }
            }

            if (!flag)
                return;
            dataset.Tables.Clear();
            dataset.Tables.AddRange(dataTableList.ToArray());
        }

        private static bool IsEmptyRow(IExcelDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; ++i)
            {
                if (reader.GetValue(i) != null)
                    return false;
            }

            return true;
        }

        private static void Update()
        {
            UpdateProgressBar();
        }

        private static void UpdateProgressBar()
        {
            var cancel = EditorUtility.DisplayCancelableProgressBar("执行中...", _describe, _rate);
            if (_rate > 1 || cancel)
            {
                stream?.Close();
                excelDataReader?.Close();
                EditorUtility.ClearProgressBar();
                EditorCoroutineRunner.Stop();
            }
        }

        public static void UpdateRate(float rate)
        {
            _rate = rate;
        }

        public static void UpdateDescribe(string describe)
        {
            _describe = describe;
        }
    }
}