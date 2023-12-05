using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    protected GameObject[] canvas;
    public GameObject rat;

    private CharacterController characterController;
    private BoxCollider boxCollider;

    /// <summary>
    /// Esto corresponde a la anecdota comentada en DeadMenu.cs
    /// <para>Esta funcion era un Awake() o un Start(), pero cuando lo cambiamos a OnEnable() todo se soluciono</para>
    /// </summary>
    private void OnEnable()
    {
        try
        {
            rat = GameManager.Instance.GetPlayer();
            //rat.GetComponent<RatController>().enabled = false;
        }
        catch { /* Para el menu principal */ }
    }

    // RESUME FUNCTION
    public void Resume()
    {
        if (rat.GetComponentInChildren<Animator>().GetBool("isDead"))
        {
            RatController.Instance.CallInvincibility(1f);
            rat.GetComponentInChildren<Animator>().SetBool("isDead", false);
        }          
        RoadTileMove.speed = -1;
        Time.timeScale = GameManager.Instance.ActualTime();
        GameManager.Instance.ResumeGame();

    }


    public IEnumerator DisableRatController()
    {
        
        yield return new WaitForSeconds(2f);
        boxCollider.enabled = true;
        Debug.Log("HAN PASADO 2 SEGUNDOS");
        
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

    // QUIT FUNCTION
    /// <summary>
    /// �Hace falta que diga que hace esta funcion?
    /// </summary>
    public void Quit()
    {
        Debug.LogWarning("Saliendo de UndeRRated");
        Application.Quit();
    }
}
