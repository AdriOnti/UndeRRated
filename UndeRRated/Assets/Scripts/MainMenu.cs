using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    private Animation animation;
    private Animator animator;

    /// <summary>
    /// Cuando el usuario le de al boton de comenzar partida, se cargara la escena 
    /// </summary>
    public void StartGame()
    {
        Time.timeScale = 1.0f;
        CameraInStart.animIsStart = true;
        animator = rat.GetComponent<Animator>();
        animator.enabled = true;

        animator.Play("startGame");

        // Se han de ejecutar la animacion de correr y la de StartGame al mismo tiempo

        //animation.Play("Armature_ArmatureAction");

        //SceneManager.LoadScene("UndeRRated");
    }

    public void RatShop()
    {
        Debug.LogWarning("FALTA LA TIENDA, FALTA LA TIENDA, FALTA LA TIENDA");
        //SceneManager.LoadScene("Shop");
    }
}
