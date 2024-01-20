using System.IO;
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

}
