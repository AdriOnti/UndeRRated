using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementName : MonoBehaviour
{
    public new TextMeshProUGUI name;
    public Image image;
    public int id;

    private void Start()
    {
        name.text = $"{DataManager.instance.ShowAchievementName(id)}";
        image = GetComponent<Image>();
        if (image != null)
        {
            if (name.text != "???") { Debug.Log(name.text); CambiarAlphaImagen(255f); }
        else CambiarAlphaImagen(80f);
        }
        else Debug.LogError("No se encontró el componente Image en el GameObject o sus hijos.");
    }

    void CambiarAlphaImagen(float alpha)
    {
        if (image != null)
        {
            Color colorActual = image.color;
            colorActual.a = alpha;
            image.color = colorActual;
        }
        else
        {
            Debug.LogError("El componente Image es nulo. Asegúrate de que esté presente en el GameObject o sus hijos.");
        }
    }
}
