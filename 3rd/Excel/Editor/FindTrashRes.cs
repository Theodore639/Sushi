using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class FindTrashRes : Editor
{
    public static string prefabPath = "Assets/Resources/";
    public static string texturePath = "";

    [MenuItem("Assets/FindTrashRes(检查未使用的图片)")]
    private static void Check()
    {
        if (Selection.activeObject != null)
        {
            if (Selection.activeObject.GetType() == typeof(DefaultAsset))
            {
                try
                {
                    texturePath = AssetDatabase.GetAssetPath((DefaultAsset) Selection.activeObject);
                    CheckTextureForPrefab();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    EditorUtility.ClearProgressBar();
                }
            }
            else if (Selection.activeObject.GetType() == typeof(Texture2D))
            {
                try
                {
                    texturePath = AssetDatabase.GetAssetPath((Texture2D) Selection.activeObject);
                    CheckTextureForPrefab(texturePath);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.StackTrace);
                }
            }
        }
    }

    private static void CheckTextureForPrefab(string path)
    {
        string log = "检查  图片：" + path + "\t预制体文件夹：" + prefabPath;
        DirectoryInfo prefabDir = new DirectoryInfo(prefabPath);
        List<String> list = new List<string>();
        FileInfo[] files = prefabDir.GetFiles("*.prefab", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            string subPath = files[i].FullName.Remove(0, Application.dataPath.Length - 6);
            GameObject obj = AssetDatabase.LoadAssetAtPath(subPath, typeof(GameObject)) as GameObject;
            Image[] images = obj.GetComponentsInChildren<Image>();
            for (int im = 0; im < images.Length; im++)
            {
                var texture = images[im].mainTexture;
                if (texture != null)
                {
                    string assetPath = AssetDatabase.GetAssetPath(texture);
                    if (assetPath.Equals(path))
                    {
                        list.Add(subPath.Remove(0, prefabPath.Length + 1) + "(" + images[im].gameObject.name + ")");
                    }
                }
            }
        }


        log += "\n\n\n被使用过的图片：";
        foreach (string prefab_path in list)
        {
            if (!string.IsNullOrEmpty(prefab_path))
            {
                log += ("\n使用的预制体：" + prefab_path);
            }
        }

        Debug.Log(log);
    }

    private static void CheckTextureForPrefab()
    {
        string log = "检查  图片文件夹：" + texturePath + "\t预制体文件夹：" + prefabPath;
        Hashtable texturePathTab = new Hashtable();
        DirectoryInfo textureDir = new DirectoryInfo(texturePath);
        DirectoryInfo prefabDir = new DirectoryInfo(prefabPath);

        //获取全部图片文件
        FileInfo[] files = textureDir.GetFiles("*.jpg", SearchOption.AllDirectories);
        EditorUtility.DisplayProgressBar("查找文件夹下所有图片", "", 0);
        for (int i = 0; i < files.Length; i++)
        {
            string subPath = files[i].FullName.Remove(0, Application.dataPath.Length - 6);
            subPath = subPath.Replace("\\", "/");
            texturePathTab.Add(subPath, "");
        }

        files = textureDir.GetFiles("*.png", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            string subPath = files[i].FullName.Remove(0, Application.dataPath.Length - 6);
            subPath = subPath.Replace("\\", "/");
            texturePathTab.Add(subPath, "");
        }

        EditorUtility.DisplayProgressBar("查找文件夹下所有图片", "", 1);

        //判断是否被使用过
        files = prefabDir.GetFiles("*.prefab", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            EditorUtility.DisplayProgressBar("检查预制体", files[i].Name, i / (float) files.Length);
            string subPath = files[i].FullName.Remove(0, Application.dataPath.Length - 6);
            GameObject obj = AssetDatabase.LoadAssetAtPath(subPath, typeof(GameObject)) as GameObject;
            Image[] images = obj.GetComponentsInChildren<Image>();
            for (int im = 0; im < images.Length; im++)
            {
                var texture = images[im].mainTexture;
                if (texture != null)
                {
                    string assetPath = AssetDatabase.GetAssetPath(texture);
                    if (texturePathTab.ContainsKey(assetPath))
                    {
                        texturePathTab[assetPath] = subPath.Remove(0, prefabPath.Length + 1);
                    }
                }
            }
        }

        //合成打印日志
        EditorUtility.DisplayProgressBar("合成打印日志", "", 0);
        log += "\n未被使用的图片：";
        foreach (string k in texturePathTab.Keys)
        {
            string prefab_path = (string) texturePathTab[k];
            if (string.IsNullOrEmpty(prefab_path))
            {
                var remove = k.Remove(0, texturePath.Length + 1);
                if (remove.StartsWith("Sprite/trash/")
                    || remove.StartsWith("trash/")
                    || remove.StartsWith("Sprite/Dessert/")
                    || remove.StartsWith("Sprite/Customer/")
                    || remove.StartsWith("Sprite/SpCustomer/")
                    || remove.StartsWith("Dessert/")
                    || remove.StartsWith("Sprite/Equipment/")
                    || remove.StartsWith("Sprite/Ico/")
                    || remove.StartsWith("Equipment/")
                )
                    continue;
                log += ("\n" + remove);
            }
        }

        log += "\n\n\n被使用过的图片：";
        foreach (string k in texturePathTab.Keys)
        {
            string prefab_path = (string) texturePathTab[k];
            if (!string.IsNullOrEmpty(prefab_path))
            {
                log += ("\n图片：" + k.Remove(0, texturePath.Length + 1) + "\t使用的预制体：" + prefab_path);
            }
        }

        EditorUtility.DisplayProgressBar("合成打印日志", "", 1);
        Debug.Log(log);
        EditorUtility.ClearProgressBar();
    }
}