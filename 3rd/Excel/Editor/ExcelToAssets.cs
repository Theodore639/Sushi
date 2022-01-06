using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Excel.Editor
{
    /// <summary>
    /// 将解析出来的Excel转化成Assets
    /// </summary>
    public class ExcelToAssets
    {
        public static readonly List<string> tables = new List<string>
        {
            "Global",
            "Equip",
            "Dish",
            "Customer",
            "Seafood",
            "Task",
            "Achievement",
            "NewGuide",
            "Collection",
        };

        public const string SourceFile = "/Doc/GameData.xlsx";
        public const string AssetFile = "Assets/Resources/GameData.asset";

        /// <summary>
        /// 根据解析的Excel创建Asset
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tables"></param>
        /// <returns></returns>
        public static IEnumerator CreatAsset(DataSet result, List<string> tables)
        {
            GameAsset gameAsset = AssetDatabase.LoadAssetAtPath<GameAsset>(AssetFile);
            if (gameAsset == null)
            {
                gameAsset = ScriptableObject.CreateInstance<GameAsset>();
                AssetDatabase.CreateAsset(gameAsset, AssetFile);
            }

            for (var i = 0; i < result.Tables.Count; i++)
            {
                EditorParseExcel.UpdateRate(0);
                var tableName = result.Tables[i].TableName;
                if (!tables.Contains(tableName))
                {
                    continue;
                }

                switch (tableName)
                {
                    case "Global":
                        //yield return LoadGlobalData(result.Tables[i], gameAsset.global,
                        //    (data) => { gameAsset.global = (GlobalAsset)data; });
                        break;
                    case "Equip":
                        yield return LoadData(result.Tables[i], gameAsset.equips);
                        break;
                    case "Item":
                        yield return LoadData(result.Tables[i], gameAsset.dishes);
                        break;
                    //case "Customer":
                    //    yield return LoadData(result.Tables[i], gameAsset.customer);
                    //    break;
                    // case "Task":
                    //    yield return LoadData(result.Tables[i], gameAsset.task);
                    //    break;
                }
            }

            EditorUtility.SetDirty(gameAsset);
            AssetDatabase.SaveAssets();
        }

        ///// <summary>
        ///// 反射加载Global数据
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="gameAssets"></param>
        //private static IEnumerator LoadGlobalData<T>(DataTable table, T gameAssets, Action<object> action)
        //{
        //    const int dataStartRow = 1; //除过标题，所在的行号
        //    var type = typeof(T);
        //    var fields = type.GetFields();
        //    gameAssets = (T) Activator.CreateInstance(type);
        //    var objects = new Dictionary<string, dynamic>();

        //    var objectsFields = new Dictionary<string, Dictionary<string, FieldInfo>>();
        //    foreach (var field in fields)
        //    {
        //        yield return field;
        //        dynamic fieldObj = Activator.CreateInstance(field.FieldType);
        //        objects[field.Name] = fieldObj;
        //        var objectsField = field.FieldType.GetFields();
        //        var infos = new Dictionary<string, FieldInfo>();
        //        foreach (var fieldInfo in objectsField)
        //        {
        //            infos[fieldInfo.Name] = fieldInfo;
        //        }

        //        objectsFields[field.Name] = infos;
        //    }

        //    var columns = new Dictionary<string, int>();
        //    for (var i = 0;
        //        i < table.Columns.Count;
        //        i++)
        //    {
        //        var data = table.Rows[dataStartRow][i];
        //        if (data != DBNull.Value)
        //            columns[(string) data] = i;
        //    }

        //    for (var i = dataStartRow + 1;
        //        i < table.Rows.Count;
        //        i++)
        //    {
        //        EditorParseExcel.UpdateDescribe("解析" + type.Name + "(" + i + "/" + table.Rows.Count + ")");
        //        EditorParseExcel.UpdateRate(i / (float) table.Rows.Count);
        //        yield return i;


        //        var groupName = table.Rows[i][columns["group"]].ToString();
        //        var paramName = table.Rows[i][columns["param"]].ToString();
        //        var value = table.Rows[i][columns["value"]].ToString();

        //        if (groupName == string.Empty)
        //        {
        //            continue;
        //        }

        //        if (paramName == string.Empty)
        //        {
        //            continue;
        //        }

        //        try
        //        {
        //            var field = objectsFields[groupName][paramName];
        //            var data = objects[groupName];
        //            if (field.FieldType == typeof(int))
        //            {
        //                data = SetModelValue(field.Name, int.Parse(value), data, data.GetType());
        //            }
        //            else if (field.FieldType == typeof(float))
        //            {
        //                data = SetModelValue(field.Name, float.Parse(value), data, data.GetType());
        //            }
        //            else if (field.FieldType == typeof(string))
        //            {
        //                data = SetModelValue(field.Name, value, data, data.GetType());
        //            }
        //            else if (field.FieldType == typeof(List<int>))
        //            {
        //                data = SetModelValue(field.Name, GetList(value),
        //                    data, data.GetType());
        //            }
        //            else if (field.FieldType == typeof(List<float>))
        //            {
        //                data = SetModelValue(field.Name, GetListFloat(value),
        //                    data, data.GetType());
        //            }
        //            else
        //            {
        //                Debug.LogError(field.FieldType + " 需要添加解析方式");
        //            }

        //            objects[groupName] = data;
        //        }
        //        catch (Exception)
        //        {
        //            Debug.LogError("类中找不到对应属性：groupName: " + groupName + "  paramName: " + paramName);
        //        }
        //    }

        //    foreach (var field in fields)
        //    {
        //        yield return field;
        //        gameAssets = SetModelValue(field.Name, objects[field.Name], gameAssets, type);
        //    }

        //    action.Invoke(gameAssets);
        //}

        /// <summary>
        /// 反射加载数据，处理一行为一项数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="gameAssets"></param>
        private static IEnumerator LoadData<T>(DataTable table, ICollection<T> gameAssets)
        {
            gameAssets.Clear();
            var fieldIndexMap = new Dictionary<string, List<int>>();
            var type = typeof(T);

            var fields = type.GetFields();
            //获取指定类所有变量名
            foreach (var field in fields)
            {
                fieldIndexMap[field.Name] = new List<int>();
                yield return field;
            }

            var dataStartRow = 3; //除过标题，所在的行号
            for (var i = 0;
                i < table.Rows.Count;
                i++)
            {
                if (table.Rows[i].ItemArray[0].ToString().Equals("index"))
                {
                    dataStartRow = i;
                    break;
                }
            }

            //获取指定变量名在表中的索引
            var itemArray = table.Rows[dataStartRow].ItemArray;
            for (var i = 0; i < itemArray.Length; i++)
            {
                var itemName = itemArray[i].ToString();
                if (itemName == string.Empty)
                {
                    continue;
                }
                try
                {
                    fieldIndexMap[itemName].Add(i);
                }
                catch (Exception)
                {
                    Debug.LogError("类中找不到相应属性{table: " + table.TableName + " field: " + itemName + "}");
                }

                yield return i;
            }

            int GetInt(int i, int value)
            {
                var s = table.Rows[i][value].ToString();
                if (s == String.Empty)
                {
                    return 0;
                }
                return int.Parse(s);
            }

            string GetString(int i, int value)
            {
                return table.Rows[i][value].ToString();
            }

            dataStartRow += 1;
            for (int i = dataStartRow; i < table.Rows.Count; i++)
            {
                EditorParseExcel.UpdateDescribe("解析" + type.Name + "(" + i + "/" + table.Rows.Count + ")");
                EditorParseExcel.UpdateRate(i / (float)table.Rows.Count);
                yield return i;
                dynamic data = Activator.CreateInstance(type);
                foreach (var field in fields)
                {
                    yield return field;
                    var list = fieldIndexMap[field.Name];
                    if (list.Count == 0)
                    {
                        Debug.LogError(field.Name + "该属性未在表" + table.TableName + "中指明列数");
                        continue;
                    }

                    try
                    {
                        if (field.FieldType == typeof(int))
                        {
                            data = SetModelValue(field.Name, GetInt(i, list[0]), data, type);
                        }
                        else if (field.FieldType == typeof(string))
                        {
                            data = SetModelValue(field.Name, GetString(i, list[0]), data, type);
                        }
                        else if (field.FieldType == typeof(List<int>))
                        {
                            data = SetModelValue(field.Name, GetList(GetString(i, list[0])),
                                data, type);
                        } 
                        //else if (field.FieldType == typeof(LocationType))
                        //{
                        //    data = SetModelValue(field.Name, (LocationType)GetInt(i, list[0]), data, type);
                        //}
                        else if (field.FieldType == typeof(ItemColor))
                        {
                            data = SetModelValue(field.Name, GetItemColor(GetString(i, list[0])), data, type);
                        }
                        else
                        {
                            Debug.LogError(field.FieldType + " 需要添加解析方式");
                        }
                    }
                    catch (Exception)
                    {
                        Debug.LogError("table:" + table.TableName + "  field:" + field.Name + "  row:" + list[0]);
                        throw;
                    }
                }

                gameAssets.Add(data);
            }
        }

        /// 
        /// 设置类中的属性值
        /// 
        public static object SetModelValue(string fieldName, object value, object obj, Type type)
        {
            if (value == null) return obj;
            try
            {
                object target = obj;
                type.GetField(fieldName).SetValue(target, Convert.ChangeType(value, type.GetField(fieldName).FieldType));
                return target;
            }
            catch (Exception)
            {
                Debug.LogError(fieldName);
                throw;
            }
        }

        ///// <summary>
        ///// 通过文字转换为EquipmentType
        ///// </summary>
        ///// <param name="typeName"></param>
        ///// <returns></returns>
        //private static EquipmentType GetEquipmentType(String typeName)
        //{
        //    switch (typeName)
        //    {
        //        case "设备":
        //            return EquipmentType.Equipment;
        //        case "装饰":
        //            return EquipmentType.Decorate;
        //        default:
        //            return EquipmentType.Special;
        //    }
        //}

        /// <summary>
        /// 通过文本转换为ItemColor
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static ItemColor GetItemColor(String typeName)
        {
            return (ItemColor)Enum.Parse(typeof(ItemColor), typeName);
        }

        /// <summary>
        /// 根据空格从字符串中提取List<int>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static List<int> GetList(String s)
        {
            List<int> result = new List<int>();
            if (s == String.Empty)
            {
                return result;
            }

            var strings = s.Split(' ');
            try

            {
                foreach (var v in strings)
                {
                    result.Add(int.Parse(v));
                }
            }
            catch (Exception)
            {
                Debug.LogError(s);
                throw;
            }

            return result;
        }

        /// <summary>
        /// 根据空格从字符串中提取List<float>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static List<float> GetListFloat(string s)
        {
            var result = new List<float>();
            if (s == string.Empty)
            {
                return result;
            }

            var strings = s.Split(' ');
            try

            {
                result.AddRange(strings.Select(float.Parse));
            }
            catch (Exception)
            {
                Debug.LogError(s);
                throw;
            }

            return result;
        }
    }
}