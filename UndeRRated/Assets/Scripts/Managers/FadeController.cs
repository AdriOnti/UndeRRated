using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static FadeController instance;
    private GameObject fadeOut;
    private GameObject fadeIn;

    private void Awake()
    {
       
        if (instance == null) instance = this;

        GameObject fadeFather = GameObject.Find("Fade");
        GameObject[] fades = new GameObject[fadeFather.transform.childCount];

        for (int i = 0; i < fades.Length; i++)
        {
            fades[i] = fadeFather.transform.GetChild(i).gameObject;
        }

        foreach (GameObject f in fades)
        {
            if (f.name == "PanelOut") fadeOut = f.gameObject;
            if (f.name == "PanelIn") fadeIn = f.gameObject;
        }
    }

    public void FadeIn() { fadeIn.SetActive(true); fadeOut.SetActive(false); }

    public void FadeOut() { fadeIn.SetActive(false); fadeOut.SetActive(true); }
}
