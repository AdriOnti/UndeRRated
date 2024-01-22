using System.IO;
using System.Linq;
using System;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public int cheeseSaved;
    //public int rainbowCost;
    //public int shieldCost;
    private string path;
    private int spentCheeses;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        path = DataManager.instance.GetPathData();
        GetSavedMoney();
    }

    public void GetSavedMoney()
    {
        string[] fileLines = DataManager.instance.ReadData();
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (DataManager.instance.Decrypt(sections[0]) == "Quesitos") cheeseSaved = Convert.ToInt32(sections[1]);

        }
    }

    public void SaveMoney()
    {
        string[] fileLines = DataManager.instance.ReadData();
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (DataManager.instance.Decrypt(sections[0]) == "Quesitos")
            {
                int cheeseTmp = cheeseSaved - spentCheeses;
                sections[1] = cheeseTmp.ToString();
                fileLines[i] = string.Join(";", sections);
                Debug.Log(fileLines[i]);


                File.WriteAllText(path, string.Empty);
                string modifiedContent = string.Join("\n", fileLines.Where(line => !string.IsNullOrWhiteSpace(line)));
                File.WriteAllText(path, modifiedContent);
            }
        }
    }
}
