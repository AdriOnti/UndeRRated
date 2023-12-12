using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static FadeController instance;
    private GameObject fadeOut;
    private GameObject fadeIn;

    private void Awake()
    {
        instance = this;
        //animator = GetComponent<Animator>();
        GameObject fadeFather = GameObject.Find("Fade");
        GameObject[] fades = new GameObject[fadeFather.transform.childCount];

        for (int i = 0; i < fades.Length; i++)
        {
            fades[i] = fadeFather.transform.GetChild(i).gameObject;
        }

        foreach (GameObject f in fades)
        {
            if (f.name == "PanelOut") fadeOut = f.gameObject;
            if(f.name == "PanelIn") fadeIn = f.gameObject;
        }

        Debug.Log(fadeIn);
        Debug.Log(fadeOut);

    }

    public void FadeIn() { fadeIn.SetActive(true); fadeOut.SetActive(false); }

    public void FadeOut() { fadeIn.SetActive(false); fadeOut.SetActive(true); }
}
