using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject[] canvas;
    [SerializeField] private float actualTime = 1.0f;
    private GameObject player;
    public int cheeseSaved;
    public int highScore;
    public int RespawnCost = 50;
    public bool stopCooldowns;

    public List<GameObject> achievements;
    // [0]100 points  [1]500 points  [2]1000 points  [3]100 bats  [4]Secret
    [HideInInspector] public bool[] achievementsBool = new bool[5];

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        player = GetPlayer();
        CanvasController();
        GetSavedMoney();
        GetHighScore();
    }

    public float ActualTime()
    {
        return actualTime;
    }

    public GameObject GetPlayer()
    {
        return GameObject.Find("RatObj");
    }

    private void CanvasController()
    {
        canvas = GetUI();
        ActiveCanvas();
        ResumeGame();
    }

    private void ActiveCanvas()
    {
        foreach (GameObject menu in canvas)
        {
            menu.SetActive(true);
        }
    }

    public void PauseGame()
    {
        actualTime = Time.timeScale;
        player.GetComponent<RatController>().enabled = false;
        ActiveUI("PauseMenu");
        //Time.timeScale= PauseMenu.pausedTime;
    }

    public void DeadCharacter()
    {
        actualTime = Time.timeScale;
        player.GetComponent<RatController>().enabled = false;
        ActiveUI("DeadMenu");
    }

    public void ResumeGame()
    {
        player.GetComponent<RatController>().enabled = true;
        Time.timeScale = actualTime;
        ActiveUI("HUD");
    }


    /// <summary>
    /// Olav: Cuando se implemento el GameManager el antiguo bug de que en el menu de muerte podias abrir el de pausa, reapareció.
    /// No tengo ni idea de como funciona esto, simplemente funciona y punto
    /// </summary>
    /// <returns>Si el menu de pausa esta desactivado o no ¿creo?</returns>
    public bool DeadMenuActive()
    {
        foreach (GameObject menu in canvas)
        {
            if (menu.name != "DeadMenu") return menu.gameObject.activeSelf;
        }

        return false;
    }

    // Activa el componente Canvas del objeto deseado y desactiva el resto
    private void ActiveUI(string ui)
    {
        foreach (GameObject menu in canvas)
        {
            if (menu.name != ui)
            {
                menu.gameObject.SetActive(false);
                if (menu.name == "HUD") stopCooldowns = true;
            }
            else
            {
                menu.gameObject.SetActive(true);
                if (ui == "HUD") stopCooldowns = false;

            }
        }
    }

    // Obtiene todos los canvas que tenga el componente UI
    public GameObject[] GetUI()
    {
        GameObject father = GameObject.Find("UI");
        GameObject[] ui = new GameObject[father.transform.childCount - 1];

        for (int i = 0; i < ui.Length; i++)
        {
            if (father.transform.GetChild(i).gameObject.name != "EventSystem") ui[i] = father.transform.GetChild(i).gameObject;
        }

        return ui;
    }

    public List<TextMeshProUGUI> ResumedAndPausedAssets(string activeMenu)
    {
        List<TextMeshProUGUI> result = new List<TextMeshProUGUI>();
        foreach (GameObject menu in canvas)
        {
            if (menu.name == "HUD")
            {
                for (int i = 0; i < menu.transform.childCount; i++)
                {
                    if (menu.transform.GetChild(i).gameObject.name == "Score") result.Add(menu.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
                    if (menu.transform.GetChild(i).gameObject.name == "Quesitos") result.Add(menu.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
                }
            }

            if (menu.name == activeMenu)
            {
                for (int i = 0; i < menu.transform.childCount; i++)
                {
                    if (menu.transform.GetChild(i).gameObject.name == "PuntuacionPaused") result.Add(menu.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
                    if (menu.transform.GetChild(i).gameObject.name == "CheeseCollected") result.Add(menu.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
                    if(menu.transform.GetChild(i).gameObject.name == "HighScore") result.Add(menu.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
                    if(menu.transform.GetChild(i).gameObject.name == "SavedCheese") result.Add(menu.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
                }
            }
        }

        return result;
    }

    // Obtener todos los objetivos que la rata puede disparar
    public Transform[] RatShotTarget()
    {
        return GetTarget("BulletTarget");
    }

    // Obtener todos los objetivos que los murcielagos pueden atacar
    public Transform[] BatTarget()
    {
        return GetTarget("AttackingPosition");
    }

    /// <summary>
    /// Obtiene todos los objetos a los que se apuntan
    /// </summary>
    /// <param name="targetName">Nombre del objetivo</param>
    /// <returns>Devuelve los objetivos a atacar</returns>    
    private Transform[] GetTarget(string targetName)
    {
        Transform[] targets = new Transform[3];
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = GameObject.Find(targetName + Convert.ToInt32(i + 1)).transform;
        }

        return targets;
    }

    public void GetSavedMoney()
    {
        string path = Path.Combine(Application.persistentDataPath, "Files/data.rat");
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();
        sr.Close();

        string[] fileLines = file.Split('\r', '\n');
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (sections[0] == "Quesitos")
            {
                cheeseSaved = Convert.ToInt32(sections[1]);
            }

        }
    }

    public void SaveMoney(int cheeseCollected, bool respawn)
    {
        string path = Path.Combine(Application.persistentDataPath, "Files/data.rat");
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();
        sr.Close();

        string[] fileLines = file.Split('\r', '\n');
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (sections[0] == "Quesitos")
            {
                if (!respawn)
                {
                    int cheeseTmp = cheeseSaved + cheeseCollected;
                    sections[1] = cheeseTmp.ToString();
                    fileLines[i] = string.Join(";", sections);
                }
                else
                {
                    cheeseCollected -= RespawnCost;
                    int cheeseTmp = cheeseSaved + cheeseCollected;
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

    private void GetHighScore()
    {
        string path = Path.Combine(Application.persistentDataPath, "Files/data.rat");
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();
        sr.Close();

        string[] fileLines = file.Split('\r', '\n');
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (sections[0] == "Score")
            {
                highScore = Convert.ToInt32(sections[1]);
            }

        }
    }

    public void SaveHighScore(int score)
    {
        string path = Path.Combine(Application.persistentDataPath, "Files/data.rat");
        StreamReader sr = File.OpenText(path);
        string file = sr.ReadToEnd();
        sr.Close();

        string[] fileLines = file.Split('\r', '\n');
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] sections = fileLines[i].Split(';');
            if (sections[0] == "Score" && score > highScore)
            {
                highScore = score;
                sections[1] = highScore.ToString();
                fileLines[i] = string.Join(";", sections);
            }
        }
        File.WriteAllText(path, string.Empty);
        string modifiedContent = string.Join("\n", fileLines.Where(line => !string.IsNullOrWhiteSpace(line)));
        File.WriteAllText(path, modifiedContent);

        GetHighScore();

    }

    public void ShowAchievement(int achievementId)
    {
        StartCoroutine(AchievementCanvas(achievementId));
    }

    private IEnumerator AchievementCanvas(int id)
    {
        achievements[id].SetActive(true);
        yield return new WaitForSeconds(5f);
        achievements[id].SetActive(false);
    }
}
