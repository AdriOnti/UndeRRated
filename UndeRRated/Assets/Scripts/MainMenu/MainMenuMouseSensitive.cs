using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuMouseSensitive : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI desc;
    public int id;
    public Image img;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Hover");
        desc.text = $"{DataManager.instance.ShowAchievementDesc(id)}";
        //CambiarAlphaImagen(255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Unhover");
        desc.text = $"";
        //CambiarAlphaImagen(80f);
    }

    void CambiarAlphaImagen(float alpha)
    {
        if (img != null)
        {
            Color colorActual = img.color;
            colorActual.a = alpha;
            img.color = colorActual;
        }
        else
        {
            Debug.LogError("El componente Image es nulo. Asegúrate de que esté presente en el GameObject o sus hijos.");
        }
    }
}
