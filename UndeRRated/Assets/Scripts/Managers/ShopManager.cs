using System.IO;
using System.Linq;
using System;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private int cheeseSaved;
    public int rainbowCost;
    public int shieldCost;
    private string path;
    private int spentCheeses;

    private void Awake()
    {
        path = DataManager.instance.GetPathData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetSavedMoney()
    {
        string[] fileLines = DataManager.instance.ReadData();
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (sections[0] == "Quesitos") cheeseSaved = Convert.ToInt32(sections[1]);

        }
    }

    public void SaveMoney()
    {
        string[] fileLines = DataManager.instance.ReadData();
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (sections[0] == "Quesitos")
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
