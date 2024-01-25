using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSensitive : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlayEnvironment(Audios.ButtonClick_1);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlayEnvironment(Audios.ButtonHover);
        this.transform.localScale = new Vector3(1.12f, 1.12f, 1.12f);
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }
}
