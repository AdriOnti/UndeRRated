using System.Collections;
using UnityEngine;

public class MainMenu : Menu
{
    private Animator animatorParent;
    private GameObject mainCanvas;
    private GameObject howToPlayCanvas;
    private GameObject shopCanvas;
    private GameObject achievementCanvas;

    private void CanvasAssigment()
    {
        foreach (GameObject canvas in GetUI())
        {
            if (canvas.gameObject.name == "MainCanvas") mainCanvas = canvas;
            if (canvas.gameObject.name == "Controles") howToPlayCanvas = canvas;
            if (canvas.gameObject.name == "Shop") shopCanvas = canvas;
            if (canvas.gameObject.name == "Achievement") achievementCanvas = canvas;
        }
    }

    private GameObject[] GetUI()
    {
        GameObject father = GameObject.Find("CanvasFather");
        GameObject[] ui = new GameObject[father.transform.childCount - 1];

        for (int i = 0; i < ui.Length; i++)
        {
            if (father.transform.GetChild(i).gameObject.name != "Fade") ui[i] = father.transform.GetChild(i).gameObject;
        }

        return ui;
    }

    protected override void OnEnable()
    {
        Time.timeScale = 1.0f;
        try
        {
            CanvasAssigment();
            mainCanvas.GetComponent<Canvas>().enabled = true;
            howToPlayCanvas.GetComponent<Canvas>().enabled = false;
            shopCanvas.GetComponent<Canvas>().enabled = false;
            achievementCanvas.GetComponent<Canvas>().enabled = false;
        }
        catch { }
    }

    /// <summary>
    /// Cuando el usuario le de al boton de comenzar partida, se cargara la escena 
    /// </summary>
    public void StartGame()
    {
        CameraInStart.Instance.animIsStart = true;
        animatorParent = rat.GetComponent<Animator>();
        mainCanvas.GetComponent<Canvas>().enabled = false;
        animatorParent.SetBool("start", true);
    }

    /// <summary>
    /// Funcion que llaman los botones del MainMenu
    /// </summary>
    /// <param name="sewer">0: MainSewer, 1: HowToPlaySewer, 2: ShopSewer, 3: AchievementSewer</param>
    public void SewerIn(int sewer)
    {
        switch(sewer)
        {
            case 0:
                StartCoroutine(MainIn());
                break;
            case 1:
            case 2:
            case 3:
                StartCoroutine(OtherSewer(DecideSewer(sewer)));
                break;
        }
    }

    /// <summary>
    /// Segun el numero de sewer, se devuelve una string utilizada por el CameraInStart;
    /// </summary>
    /// <param name="sewer"></param>
    /// <returns></returns>
    private string DecideSewer(int sewer)
    {
        switch (sewer)
        {
            case 1:
                return "HowToPlayIn";
            case 2:
                return "RatShopIn";
            case 3:
                return "AchievementIn";
        }
        return null;
    }

    /// <summary>
    /// Corrutina que permite el cambio de sewer
    /// </summary>
    /// <param name="sewer">Nombre del bool que se quiere</param>
    IEnumerator OtherSewer(string sewer)
    {
        DisableMainCanvas();
        yield return new WaitForSeconds(0.5f);
        CameraInStart.Instance.ModifyBools(sewer);

        yield return new WaitForSeconds(0.5f);
        DecideCanvas(sewer).GetComponent<Canvas>().enabled = true;
        FadeController.instance.FadeIn();
    }

    private GameObject DecideCanvas(string sewer)
    {
        switch (sewer)
        {
            case "HowToPlayIn":
                return howToPlayCanvas;
            case "RatShopIn":
                return shopCanvas;
            case "AchievementIn":
                return achievementCanvas;
        }
        return null;
    }

    IEnumerator MainIn()
    {
        FadeController.instance.FadeOut();
        howToPlayCanvas.GetComponent<Canvas>().enabled = false;
        shopCanvas.GetComponent<Canvas>().enabled = false;
        achievementCanvas.GetComponent<Canvas>().enabled = false;

        yield return new WaitForSeconds(0.5f);
        CameraInStart.Instance.ModifyBools("MainSectionIn");

        yield return new WaitForSeconds(0.5f);
        mainCanvas.GetComponent<Canvas>().enabled = true;
        FadeController.instance.FadeIn();
    }

    private void DisableMainCanvas()
    {
        // ¿Tienes el GameObject Fade desactivado?
        FadeController.instance.FadeOut();
        mainCanvas.GetComponent<Canvas>().enabled = false;
    }
}
