using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Excel.Editor;
using ExcelDataReader;
using I2.Loc;
using UnityEditor;
using UnityEngine;

namespace _3rd.Excel.Editor
{
    /// <summary>
    /// 导出I2语言
    /// </summary>
    public class EditorExportI2Language
    {
        private static float _rate;
        private static string _describe = "开始解析";
        private static string I2Languages = "Assets/Resources/I2Languages.asset";
        private static string LanguageDic = "Assets/Resources/Languages/";
        private static FileStream fsCode;
        private static FileStream fs;

        [MenuItem("Tools/ExportI2Language")]
        private static void ExportI2Language()
        {
            _rate = 0;
            _describe = "开始解析";
            EditorCoroutineRunner.StartEditorCoroutine(Export());
            EditorApplication.update += Update;
        }

        private static IEnumerator Export()
        {
            fs = new FileStream("Assets/3rd/Font/结果字符集.txt", FileMode.Create);

            _describe = "写入字符集...";
            fsCode = new FileStream("Assets/3rd/Font/字符集.txt", FileMode.Open);
            WriteCode(0.1f);
            fsCode.Close();
            fsCode = null;
            _rate = 0.1f;
            yield return _rate;

            var sb = new StringBuilder();
            _describe = "读取i2Languages...";
            var i2Languages = AssetDatabase.LoadAssetAtPath<LanguageSourceAsset>(I2Languages);
            var sourceI2 = i2Languages.mSource;
            sb.Append("\n");
            sb.Append("i2Languages");
            sb.Append("\n");
            Read(sourceI2, 0.15f);
            _rate = 0.2f;
            yield return _rate;
            _describe = "写入i2Languages...";
            Write(0.2f);
            sb.Clear();
            _rate = 0.2f;
            yield return _rate;

            //获取指定路径下面的所有资源文件
            if (Directory.Exists(LanguageDic))
            {
                var direction = new DirectoryInfo(LanguageDic);
                var files = direction.GetFiles("*", SearchOption.AllDirectories);
                var filesLength = files.Length;
                var ratePre = 1 / (float) filesLength * (0.9f - _rate);
                for (var index = 0; index < filesLength; index++)
                {
                    var t = files[index];
                    var endRate = ratePre + _rate;
                    if (t.Name.EndsWith(".prefab"))
                    {
                        var gameObject =
                            AssetDatabase.LoadAssetAtPath<GameObject>(
                                t.FullName.Replace("\\", "/").Replace(Application.dataPath, "Assets"));
                        var languageSource = gameObject.GetComponent<LanguageSource>();
                        _describe = "读取" + gameObject.name + "...";
                        sb.Append("\n");
                        sb.Append(gameObject.name);
                        sb.Append("\n");
                        var readRate = _rate + ratePre / 2;
                        Read(languageSource.mSource, readRate);
                        _rate = readRate;
                        yield return _rate;
                        _describe = "写入" + gameObject.name + "...";
                        Write(endRate);
                        sb.Clear();
                    }

                    _rate = endRate;
                    yield return _rate;
                }
            }

            fs.Close();
            fs = null;
            _rate = 1.1f;
            yield return _rate;

            void WriteCode(float endRate)
            {
                var data2 = new byte[fsCode.Length];
                // 据字符集，将其写入结果字符集
                fsCode.Read(data2, 0, data2.Length);
                fs.Write(data2, 0, data2.Length);
            }

            void Read(LanguageSourceData source, float endRate)
            {
                var languageIndex = new List<int>();
                for (var index = 0; index < source.mLanguages.Count; index++)
                {
                    var mLanguage = source.mLanguages[index];
                    if (mLanguage.Code.Equals("zh-CN"))
                    {
                        languageIndex.Add(index);
                    }
                    else if (mLanguage.Code.Equals("zh-TW"))
                    {
                        languageIndex.Add(index);
                    }
                }

                foreach (var term in source.mTerms)
                {
                    var isAppend = false;
                    foreach (var index in languageIndex)
                    {
                        var termLanguage = term.Languages[index];
                        if (!string.Empty.Equals(termLanguage))
                        {
                            isAppend = true;
                            sb.Append(termLanguage);
                            sb.Append(" ");
                        }
                    }

                    if (isAppend)
                        sb.Append("\n");
                }
            }

            void Write(float endRate)
            {
                var bytes = new UTF8Encoding().GetBytes(sb.ToString());
                fs.Write(bytes, 0, bytes.Length);
            }
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
                fs?.Close();
                fsCode?.Close();
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