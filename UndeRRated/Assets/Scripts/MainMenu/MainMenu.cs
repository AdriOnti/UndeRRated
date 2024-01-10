using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    //private Animation animation;
    private Animator animatorParent;
    public GameObject cntrlCanvas;
    public GameObject shopCanvas;

    private void OnEnable()
    {
        Time.timeScale = 1.0f;
        try
        {
            cntrlCanvas.GetComponent<Canvas>().enabled = false;
            shopCanvas.GetComponent<Canvas>().enabled = false;
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
        //Debug.LogWarning("FALTA LA TIENDA, FALTA LA TIENDA, FALTA LA TIENDA");
        // X:      Y:       Z: 
        StartCoroutine(ShopIn());
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
        CameraInStart.ShopSectionIn = false;

        yield return new WaitForSeconds(0.5f);
        cntrlCanvas.GetComponent<Canvas>().enabled = true;
        FadeController.instance.FadeIn();
    }

    IEnumerator CntrlOut()
    {
        FadeController.instance.FadeOut();
        cntrlCanvas.GetComponent<Canvas>().enabled = false;
        shopCanvas.GetComponent<Canvas>().enabled = false;

        yield return new WaitForSeconds(0.5f);
        CameraInStart.ControlSectionOut = true;
        CameraInStart.ControlSectionIn = false;
        CameraInStart.ShopSectionIn = false;

        yield return new WaitForSeconds(0.5f);
        GameObject canvas = GameObject.Find("MainCanvas");
        canvas.GetComponent<Canvas>().enabled = true;
        FadeController.instance.FadeIn();
    }

    IEnumerator ShopIn()
    {
        FadeController.instance.FadeOut();
        GameObject canvas = GameObject.Find("MainCanvas");
        canvas.GetComponent<Canvas>().enabled = false;

        yield return new WaitForSeconds(0.5f);
        CameraInStart.ShopSectionIn = true;
        CameraInStart.ControlSectionIn = false;
        CameraInStart.ControlSectionOut = false;

        yield return new WaitForSeconds(0.5f);
        shopCanvas.GetComponent<Canvas>().enabled = true;
        FadeController.instance.FadeIn();
    }
}
