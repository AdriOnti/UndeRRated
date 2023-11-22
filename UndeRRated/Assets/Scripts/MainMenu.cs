using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    /// <summary>
    /// Cuando el usuario le de al boton de comenzar partida, se cargara la escena 
    /// </summary>
    public void StartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("UndeRRated");
    }

    public void RatShop()
    {
        Debug.LogWarning("FALTA LA TIENDA, FALTA LA TIENDA, FALTA LA TIENDA");
        //SceneManager.LoadScene("Shop");
    }
}
