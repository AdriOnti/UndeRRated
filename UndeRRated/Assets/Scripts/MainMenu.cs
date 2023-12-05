using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    //private Animation animation;
    private Animator animatorParent;

    private void OnEnable()
    {

    }

    /// <summary>
    /// Cuando el usuario le de al boton de comenzar partida, se cargara la escena 
    /// </summary>
    public void StartGame()
    {
 
        CameraInStart.animIsStart = true;
        animatorParent = rat.GetComponent<Animator>();


        GameObject canvas = GameObject.Find("MainCanvas");
        canvas.GetComponent<Canvas>().enabled = false;
        animatorParent.SetBool("start", true);
    }

    public void RatShop()
    {
        Debug.LogWarning("FALTA LA TIENDA, FALTA LA TIENDA, FALTA LA TIENDA");
        //SceneManager.LoadScene("Shop");
    }

    public void ControlSewer()
    {
        // La camara se mete en el rio, se teletransporta y vuelve a su posicion original activando el canvas de los controles
        // CameraPos = new Vector3(-52.37f, 4.62f, 37.18f);
        // CameraRot = Tranform.Rotate(19.169f, 21.943f, 0.451f);
    }
}
