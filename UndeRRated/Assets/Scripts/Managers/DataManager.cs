using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    //private int keyValue = 38;      // R + A + T == 18 + 0 + 20     | Existiendo la Ñ
    private int keyValue = 36;        // R + A + T == 17 + 0 + 19     | Sin la Ñ 

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
        string path = GetPathData();
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();
        sr.Close();

        return file.Split('\r', '\n');
    }

    public string GetPathData()
    {
        return Path.Combine(Application.persistentDataPath, "Files/data.rat");
    }

    public string Encrypt(string msg, bool isDec)
    {
        if (isDec) keyValue = -keyValue;
        //string alphabet = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        char[] originalCharacters = msg.ToCharArray();
        char[] ciphedCharacters = new char[originalCharacters.Length];

        for (int i = 0; i < originalCharacters.Length; i++)
        {
            char originalChar = originalCharacters[i];

            if (char.IsLetter(originalChar))
            {
                // Differences between Upper & Lower
                string actualLetter = char.IsUpper(originalChar) ? alphabet : alphabet.ToLower();

                int originalIndex = actualLetter.IndexOf(originalChar);

                int ciphedIndex = (originalIndex + keyValue) % alphabet.Length;      // Apply scrolling and handle overflow

                if (ciphedIndex < 0) ciphedIndex += alphabet.Length;

                ciphedCharacters[i] = actualLetter[ciphedIndex];
            }
            else ciphedCharacters[i] = originalChar;    // Keep non-alphabetic characters unchanged
        }

        return new string(ciphedCharacters);
    }

    public string Decrypt(string ciphedText) { return Encrypt(ciphedText, true); }

    public string[] ReadAchievement()
    {
        string path = Path.Combine(Application.persistentDataPath, "Files/achievement.rat");
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();
        sr.Close();

        return file.Split('\r', '\n');
    }

    public string ShowAchievementDesc(int id)
    {
        string[] fileLines = ReadAchievement();
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split('=');
            if (sections[0] == $"{id}" && sections[1] == "true")
            {
                return sections[3];
            }
        }
        
        foreach(string line in fileLines)
        {
            string[] sections = line.Split('=');
            if (sections[0] == "0") return sections[3];
        }

        return null;
    }

    public string ShowAchievementName(int id)
    {
        string[] fileLines = ReadAchievement();
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split('=');
            if (sections[0] == $"{id}" && sections[1] == "true")
            {
                return sections[2];
            }
        }

        foreach (string line in fileLines)
        {
            string[] sections = line.Split('=');
            if (sections[0] == "0") return sections[2];
        }

        return null;
    }

    public void SaveAchievement(int id)
    {
        id += 1;
        string[] fileLines = ReadAchievement();
        for(int i = 0; i < fileLines.Length;i++)
        {
            string[] sections = fileLines[i].Split('=');
            if (sections[0] == $"{id}" && sections[1] == "false")
            {
                sections[1] = "true";
                fileLines[i] = string.Join("=", sections);

                string path = Path.Combine(Application.persistentDataPath, "Files/achievement.rat");
                File.WriteAllText(path, string.Empty);
                string modifiedContent = string.Join("\n", fileLines.Where(line => !string.IsNullOrWhiteSpace(line)));
                File.WriteAllText(path, modifiedContent);
            }
        }
    }

    public bool[] GetAchievement()
    {
        string[] fileLines = ReadAchievement();
        bool[] achievements = new bool[fileLines.Length];
        for(int i = 0;  i < fileLines.Length;i++)
        {
            string[] sections = fileLines[i].Split('=');
            if (sections[0] != "0")
            {
                if (sections[1] == "true")
                {
                    Debug.Log(sections[2]);
                    achievements[i] = true;
                }
                else
                {
                    achievements[i] = false;
                }
            }
        }

        return achievements;
    }

}
