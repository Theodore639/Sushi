using System.Collections.Generic;
using System.Linq;
using Excel.Editor;
using UnityEditor;
using UnityEngine;

namespace _3rd.Excel.Editor
{
    public class ExcelSpecificWindow : EditorWindow
    {
        private Vector2 scrollPos = new Vector2(0, 0);
        private bool IsAllOperation;
        private static Dictionary<string, bool> mapDic;
        private static GUIStyle textStyle;

        public static void ShowWindow()
        {
            var window = GetWindow<ExcelSpecificWindow>();
            mapDic = new Dictionary<string, bool>();
            foreach (var tableName in ExcelToAssets.tables)
            {
                mapDic[tableName] = false;
            }

            textStyle = new GUIStyle("HeaderLabel") {alignment = TextAnchor.MiddleLeft};
            window.titleContent = new GUIContent("ExcelSpecific");
            window.Show();
        }


        private void OnGUI()
        {
            textStyle.fontSize = 25;
            GUILayout.BeginHorizontal();
            GUILayout.Label("  表名", textStyle, GUILayout.Width(100));
            GUILayout.Label("全选", textStyle, GUILayout.Width(50));
            var tempIsAll = IsAllOperation;
            tempIsAll = GUILayout.Toggle(tempIsAll, "", GUILayout.Width(100), GUILayout.Height(37.5f));
            if (tempIsAll != IsAllOperation)
            {
                IsAllOperation = tempIsAll;
                foreach (var child in ExcelToAssets.tables)
                {
                    mapDic[child] = IsAllOperation;
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();


            scrollPos = GUILayout.BeginScrollView(scrollPos);
            textStyle.fontSize = 16;
            foreach (var map in ExcelToAssets.tables)
            {
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                GUILayout.Label("  " + map, textStyle, GUILayout.Width(150));
                var isOperation = mapDic[map];
                isOperation = GUILayout.Toggle(isOperation, "", GUILayout.Width(100), GUILayout.Height(24)
                );
                mapDic[map] = isOperation;
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
            GUILayout.Space(5);

            if (GUILayout.Button("解析", GUILayout.Width(60)))
            {
                Close();
                var choose = (from keyValue in mapDic where keyValue.Value select keyValue.Key).ToList();
                EditorParseExcel.ReadGameData(choose);
            }
        }
    }
}