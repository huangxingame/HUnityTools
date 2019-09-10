/****************************************************
	文件：QuickFolderToolbar.cs
	作者：Oscar
	邮箱: 1686506972@qq.com
	日期：2019/09/06 17:37   	
	功能：常用unity路径
*****************************************************/
using System.IO;
using UnityEditor;
using UnityEngine;

public class QuickFolderToolbar
{
    private const int CatalogNodePriority = 800;
    private const string CatalogNodeName = "Tools/Quick Folder Opener/";

    /// <summary>
    /// 打开 Data Path 文件夹。
    /// </summary>
    [MenuItem(CatalogNodeName + "Application.dataPath", false, CatalogNodePriority + 103)]
    private static void OpenDataPath()
    {
        RevealInFinder(Application.dataPath);
    }

    /// <summary>
    /// 打开 Persistent Data Path 文件夹。
    /// </summary>
    [MenuItem(CatalogNodeName + "Application.persistentDataPath", false, CatalogNodePriority + 101)]
    private static void OpenPersistentDataPath()
    {
        RevealInFinder(Application.persistentDataPath);
    }

    /// <summary>
    /// 打开 Streaming Assets Path 文件夹。
    /// </summary>
    [MenuItem(CatalogNodeName + "Application.streamingAssetsPath", false, CatalogNodePriority + 102)]
    private static void OpenStreamingAssets()
    {
        RevealInFinder(Application.streamingAssetsPath);
    }

    /// <summary>
    /// 打开 Temporary Cache Path 文件夹。
    /// </summary>
    [MenuItem(CatalogNodeName + "Application.temporaryCachePath", false, CatalogNodePriority + 100)]
    private static void OpenCachePath()
    {
        RevealInFinder(Application.temporaryCachePath);
    }

    [MenuItem(CatalogNodeName + "Asset Store Packages Folder", false, CatalogNodePriority + 200)]
    private static void OpenAssetStorePackagesFolder()
    {
        //http://answers.unity3d.com/questions/45050/where-unity-store-saves-the-packages.html
        //
#if UNITY_EDITOR_OSX
            string path = GetAssetStorePackagesPathOnMac();
#elif UNITY_EDITOR_WIN
        string path = GetAssetStorePackagesPathOnWindows();
#endif

        RevealInFinder(path);
    }

    [MenuItem(CatalogNodeName + "Editor Application Path", false, CatalogNodePriority + 200)]
    private static void OpenUnityEditorPath()
    {
        RevealInFinder(new FileInfo(EditorApplication.applicationPath).Directory.FullName);
    }

    [MenuItem(CatalogNodeName + "Editor Log Folder", false, CatalogNodePriority + 200)]
    private static void OpenEditorLogFolderPath()
    {
#if UNITY_EDITOR_OSX
			string rootFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var libraryPath = Path.Combine(rootFolderPath, "Library");
			var logsFolder = Path.Combine(libraryPath, "Logs");
			var UnityFolder = Path.Combine(logsFolder, "Unity");
			RevealInFinder(UnityFolder);
#elif UNITY_EDITOR_WIN
        var rootFolderPath = System.Environment.ExpandEnvironmentVariables("%localappdata%");
        var unityFolder = Path.Combine(rootFolderPath, "Unity");
        RevealInFinder(Path.Combine(unityFolder, "Editor"));
#endif
    }

    [MenuItem(CatalogNodeName + "Asset Backup Folder", false, CatalogNodePriority + 300)]
    public static void OpenAEBackupFolder()
    {
        var folder = Path.Combine(Application.persistentDataPath, "AEBackup");
        Directory.CreateDirectory(folder);
        RevealInFinder(folder);
    }

    private const string ASSET_STORE_FOLDER_NAME = "Asset Store-5.x";

    private static string GetAssetStorePackagesPathOnMac()
    {
        var rootFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        var libraryPath = Path.Combine(rootFolderPath, "Library");
        var unityFolder = Path.Combine(libraryPath, "Unity");
        return Path.Combine(unityFolder, ASSET_STORE_FOLDER_NAME);
    }

    private static string GetAssetStorePackagesPathOnWindows()
    {
        var rootFolderPath = System.Environment.ExpandEnvironmentVariables("%appdata%");
        var unityFolder = Path.Combine(rootFolderPath, "Unity");
        return Path.Combine(unityFolder, ASSET_STORE_FOLDER_NAME);
    }

    private static void RevealInFinder(string folder)
    {
        if (!Directory.Exists(folder))
        {
            UnityEngine.Debug.LogWarning(string.Format("Folder '{0}' is not Exists", folder));
            return;
        }

        EditorUtility.RevealInFinder(folder);
        //folder = string.Format("\"{0}\"", folder);
        //switch (Application.platform)
        //{
        //    case RuntimePlatform.WindowsEditor:
        //        Process.Start("Explorer.exe", folder.Replace('/', '\\'));
        //        break;

        //    case RuntimePlatform.OSXEditor:
        //        Process.Start("open", folder);
        //        break;

        //    default:
        //        throw new Exception(string.Format("Not support open folder on '{0}' platform.", Application.platform.ToString()));
        //}
    }
}
