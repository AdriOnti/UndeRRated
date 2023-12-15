using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

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

}
