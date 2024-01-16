using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Create the AppData folder to copy data
        string sourcePath = Path.Combine(Application.streamingAssetsPath, "Files");
        string targetPath = Path.Combine(Application.persistentDataPath, "Files");

        if (!Directory.Exists(Application.persistentDataPath)) { Directory.CreateDirectory(Application.persistentDataPath); }
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
            string[] filesS = Directory.GetFiles(sourcePath);

            foreach (string file in filesS)
            {
                string targetFile = Path.Combine(targetPath, Path.GetFileName(file));
                File.Copy(file, targetFile);
            }
        }

        string[] files = Directory.GetFiles(sourcePath);
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string targetFile = Path.Combine(targetPath, fileName);

            if (!File.Exists(targetFile)) { File.Copy(file, targetFile); }
        }
    }

    public string[] ReadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "Files/data.rat");
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();
        sr.Close();

        return file.Split('\r', '\n');
    }

    public string GetPathData()
    {
        return Path.Combine(Application.persistentDataPath, "Files/data.rat");
    }

}
