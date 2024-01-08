using UnityEngine;
using UnityEngine.EventSystems;

public class ClickeableObj : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject clickeableObj;
    public Light lightOfClickObj;
    public void OnPointerEnter(PointerEventData eventData)
    {
        clickeableObj.GetComponentInChildren<Light>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        clickeableObj.GetComponentInChildren<Light>().enabled = false;
    }
}
