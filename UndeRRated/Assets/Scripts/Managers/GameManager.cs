using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject[] canvas;
    [SerializeField] private float actualTime = 1.0f;
    public GameObject player;
    public int cheeseSaved;
    private int highScore;
    public bool stopCooldowns;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        CanvasController();
        player = GetPlayer();
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

    private void GetSavedMoney()
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

    public void SaveMoney(int cheeseCollected)
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
                int cheeseTmp = cheeseSaved + cheeseCollected;
                sections[1] = cheeseTmp.ToString();
                fileLines[i] = string.Join(";", sections);
            }
        }
        File.WriteAllText(path, string.Empty);
        string modifiedContent = string.Join("\n", fileLines.Where(line => !string.IsNullOrWhiteSpace(line)));
        File.WriteAllText(path, modifiedContent);

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

    }
}
