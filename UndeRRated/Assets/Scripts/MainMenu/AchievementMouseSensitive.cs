using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AchievementMouseSensitive : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
        desc.text = $"{DataManager.instance.ShowAchievementDesc(id)}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        desc.text = $"";
    }
}
