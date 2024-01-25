using UnityEngine;
using UnityEngine.EventSystems;

public class ClickeableObj : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject clickeableObj;
    public Light lightOfClickObj;

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlayEnvironment(Audios.ButtonClick_1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        clickeableObj.GetComponentInChildren<Light>().enabled = true;
        SoundManager.Instance.PlayEnvironment(Audios.ButtonHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        clickeableObj.GetComponentInChildren<Light>().enabled = false;
    }
}
