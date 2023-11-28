using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public List<GameObject> canvas;
    public GameObject rat;

    private CharacterController characterController;
    private BoxCollider boxCollider;
    private void OnEnable()
    {
        rat.GetComponent<RatController>().enabled = false;
        characterController = rat.GetComponent<CharacterController>();
        boxCollider = rat.GetComponent<BoxCollider>();
    }

    // RESUME FUNCTION
    public void Resume()
    {
        if (rat.GetComponentInChildren<Animator>().GetBool("isDead"))
        {
            rat.GetComponent<RatController>().enabled = false;
            StartCoroutine(DisableRatController());
        }
            

        foreach (GameObject menu in canvas)
        {
            if (menu.name == "HUD") menu.SetActive(true);
            else menu.SetActive(false);
        }

        rat.GetComponent<RatController>().enabled = true;
        RoadTileMove.speed = -1;
        Time.timeScale = 1f;
        rat.GetComponentInChildren<Animator>().SetBool("isDead", false);
        
    }


    private IEnumerator DisableRatController()
    {
        rat.GetComponent<RatController>().enabled = true;
        yield return new WaitForSeconds(2f);
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

    public void Quit()
    {
        Debug.LogWarning("Saliendo de UndeRRated");
        Application.Quit();
    }
}
