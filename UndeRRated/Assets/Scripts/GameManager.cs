using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject[] canvas;
    private float actualTime = 1.0f;
    public GameObject player;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        CanvasController();
        player = GetPlayer();
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
        //player.GetComponent<RatController>().enabled = false;
        ActiveUI("PauseMenu");
        //Time.timeScale= PauseMenu.pausedTime;
    }

    public void DeadCharacter()
    {
        actualTime = Time.timeScale;
        //player.GetComponent<RatController>().enabled = false;
        ActiveUI("DeadMenu");
    }

    public void ResumeGame()
    {
        //player.GetComponent<RatController>().enabled = true;
        Time.timeScale = actualTime;
        ActiveUI("HUD");
    }


    /// <summary>
    /// Olav: Cuando se implemento el GameManager el antiguo bug de que en el menu de muerte podias abrir el de pausa, reapareci�.
    /// No tengo ni idea de como funciona esto, simplemente funciona y punto
    /// </summary>
    /// <returns>Si el menu de pausa esta desactivado o no �creo?</returns>
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
            if (menu.name != ui) menu.gameObject.SetActive(false);
            else menu.gameObject.SetActive(true);
        }
    }

    // Obtiene todos los canvas que tenga el componente UI
    public GameObject[] GetUI()
    {
        GameObject father = GameObject.Find("UI");
        GameObject[] ui = new GameObject[father.transform.childCount - 1];

        for(int i=0;i<ui.Length;i++)
        {
            if(father.transform.GetChild(i).gameObject.name != "EventSystem") ui[i] = father.transform.GetChild(i).gameObject;
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
}
