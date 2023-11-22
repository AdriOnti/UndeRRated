using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public List<GameObject> canvas;
    public GameObject rat;

    private void OnEnable()
    {
        rat.GetComponent<RatController>().enabled = false;
    }

    // RESUME FUNCTION
    public void Resume()
    {
        foreach(GameObject menu in canvas)
        {
            if (menu.name == "HUD") menu.SetActive(true);
            else menu.SetActive(false);
        }
        RoadTileMove.speed = -1;
        //Time.timeScale = 1.0f;
        rat.GetComponentInChildren<Animator>().SetBool("isDead", false);
        rat.GetComponent<RatController>().enabled = true;
    }

    // RESTART FUNCTION
    /// <summary>
    /// Carga la escena UndeRRated para comenzar una nueva partida
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1f;
        RoadTileMove.speed = -1;
        if (SceneManager.GetActiveScene().name != "UndeRRated") SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else SceneManager.LoadScene("UndeRRated");
    }

    public void Quit()
    {
        Debug.LogWarning("Saliendo de UndeRRated");
        Application.Quit();
    }
}
