using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// using 
public class CardAssets : ScriptableObject
{
    #region MenuItem
    protected const string ResourcesPath = "Assets/Resources/";
    protected const string FolderName = "Card/";
    protected const string AssetName = nameof(CardAssets);
    protected const string dotAsset = ".asset";

#if UNITY_EDITOR
    [MenuItem("BlackJack/" + FolderName + AssetName)]
    public static CardAssets CreateAsset()
    {
        if (Resources.Load(FolderName + AssetName) != null)
        {
            var data = Resources.Load<CardAssets>(FolderName + AssetName);
            Selection.activeObject = data;
            return data;
        }

        string[] paths = (ResourcesPath + FolderName).TrimEnd('/').Split('/');
        for (int i = 1; i < paths.Length; i++)
        {
            string path = CombinePath(paths, i);
            string parentFolder = CombinePath(paths, i - 1);
            string targetFolder = paths[i];
            if (System.IO.Directory.Exists(path) == false)
                AssetDatabase.CreateFolder(parentFolder, targetFolder);
        }
        // var gameData = CreateResourceAsset<CardAssets>(ResourcesPath + ConfigFullPath + dotAsset);

        CardAssets asset = CreateInstance<CardAssets>();
        AssetDatabase.CreateAsset(asset, (ResourcesPath + FolderName + AssetName) + dotAsset);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
        return asset;
    }

    public static string CombinePath(string[] paths, int index)
    {
        string path = "";
        for (int i = 0; i <= index; i++)
        {
            path += paths[i];
            // path += (i == index) ? "" : "/";
            if (i != index)
            {
                path += "/";
            }
        }
        return path;
    }
#endif
    #endregion
    #region Singleton
    private static CardAssets instance = null;
    public static CardAssets Instance
    {
        get
        {
            if (instance == null)
            {
                try
                {
                    instance = Resources.Load<CardAssets>(ResourcesPath + FolderName + AssetName);
                }
                catch
                {
                    throw new Exception("Unable to load FoodConfig");
                }
            }
#if UNITY_EDITOR
            if (instance == null)
            {
                instance = CreateAsset();
            }
#endif
            return instance;
        }
    }
    #endregion
    public Card cardPrefab;
    public List<Sprite> suitCardIcon;
    public Card CraeteCard()
    {
        return Instantiate(CardAssets.Instance.cardPrefab);
    }
}
