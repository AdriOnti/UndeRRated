using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSensitive2 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Button clicked");
        SoundManager.Instance.PlaySound(Audios.ButtonClick_1);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1.12f, 1.12f, 1.12f);
        SoundManager.Instance.PlaySound(Audios.ButtonClick_1);
        Debug.Log("Button hovered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1, 1, 1);
        //SoundManager.Instance.StopSound();
    }
}