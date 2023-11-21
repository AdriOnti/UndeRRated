using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public List<GameObject> canvas;
    public GameObject rat;


    /// <summary>
    /// Esto corresponde a la anecdota comentada en DeadMenu.cs
    /// <para>Esta funcion era un Awake() o un Start(), pero cuando lo cambiamos a OnEnable() todo se soluciono</para>
    /// </summary>
    private void OnEnable()
    {
        try
        {
            rat.GetComponent<RatController>().enabled = false;
        }
        catch { /* Para el menu principal */ }
    }

    // RESUME FUNCTION
    public void Resume()
    {
        foreach(GameObject menu in canvas)
        {
            if (menu.name == "HUD") menu.SetActive(true);
            else menu.SetActive(false);
        }
        Time.timeScale = 1.0f;
        rat.GetComponent<RatController>().enabled = true;
    }

    // RESTART FUNCTION
    /// <summary>
    /// Carga la escena UndeRRated para comenzar una nueva partida
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name != "UndeRRated") SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else SceneManager.LoadScene("UndeRRated");
    }

    // QUIT FUNCTION
    /// <summary>
    /// ¿Hace falta que diga que hace esta funcion?
    /// </summary>
    public void Quit()
    {
        Debug.LogWarning("Saliendo de UndeRRated");
        Application.Quit();
    }
}
