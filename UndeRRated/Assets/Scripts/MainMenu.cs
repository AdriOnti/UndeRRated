using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    //private Animation animation;
    private Animator animatorParent;
    public GameObject cntrlCanvas;

    private void OnEnable()
    {
        Time.timeScale = 1.0f;
        try
        {
            cntrlCanvas.SetActive(false);
        }
        catch { }
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
    }

    public void ControlSewer() { StartCoroutine(CntrlIn()); }

    public void MainSewer() { StartCoroutine(CntrlOut()); }

    IEnumerator CntrlIn()
    {
        FadeController.instance.FadeOut();
        GameObject canvas = GameObject.Find("MainCanvas");
        canvas.GetComponent<Canvas>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        CameraInStart.ControlSectionIn = true;
        CameraInStart.ControlSectionOut = false;
        yield return new WaitForSeconds(0.5f);
        cntrlCanvas.SetActive(true);
        FadeController.instance.FadeIn();
    }

    IEnumerator CntrlOut()
    {
        FadeController.instance.FadeOut();
        cntrlCanvas.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        CameraInStart.ControlSectionOut = true;
        CameraInStart.ControlSectionIn = false;
        yield return new WaitForSeconds(0.5f);
        GameObject canvas = GameObject.Find("MainCanvas");
        canvas.GetComponent<Canvas>().enabled = true;
        FadeController.instance.FadeIn();
    }
}
