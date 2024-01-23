using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSensitive : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Button clicked");
        SoundManager.Instance.PlaySound(Audios.ButtonClick_1);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(Audios.ButtonClick_2);
        this.transform.localScale = new Vector3(1.12f, 1.12f, 1.12f);
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1, 1, 1);
        //SoundManager.Instance.StopSound();
    }
}
