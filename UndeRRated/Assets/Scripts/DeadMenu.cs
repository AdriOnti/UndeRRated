using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    public static DeadMenu instance;
    public List<GameObject> canvas;
    public GameObject rat;

    private void Start()
    {
        rat.GetComponent<RatController>().enabled = false;
    }


    // RESPAWN FUNCTION
    /// <summary>
    /// FUNCION EN PROCESO.
    /// Cuando este funcional, a cambio de una cantidad de monedas podras revivir, 
    /// para entonces esto estara en la escena UndeRRated como un canvas y no en otra escena
    /// </summary>
    public void Respawn()
    {
        Debug.LogWarning("FALTA AÑADIRLE EL COSTE");
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
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
}
