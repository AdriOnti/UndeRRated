using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuMouseSensitive : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI desc;
    public int id;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Hover");
        //desc.text = $"Hover";
        desc.text = $"{DataManager.instance.ShowAchievementDesc(id)}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Unhover");
        desc.text = $"";
    }
}
