using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    //private Animation animation;
    private Animator animator;

    /// <summary>
    /// Cuando el usuario le de al boton de comenzar partida, se cargara la escena 
    /// </summary>
    public void StartGame()
    {
        CameraInStart.animIsStart = true;
        animator = rat.GetComponent<Animator>();
        animator.enabled = true;

        GameObject canvas = GameObject.Find("Canvas");
        canvas.GetComponent<Canvas>().enabled = false;

        StartCoroutine(LoadUndeRRated());
    }

    IEnumerator LoadUndeRRated()
    {
        animator.Play("startGame");
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("UndeRRated");
    }

    public void RatShop()
    {
        Debug.LogWarning("FALTA LA TIENDA, FALTA LA TIENDA, FALTA LA TIENDA");
        //SceneManager.LoadScene("Shop");
    }
}
