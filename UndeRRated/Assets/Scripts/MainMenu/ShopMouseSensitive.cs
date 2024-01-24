using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopMouseSensitive : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI shopperTxt;
    public int id;

    public void OnPointerClick(PointerEventData eventData)
    {
        //switch (id)
        //{
        //    case 0:
        //        ShopManager.Instance.BuyShield();
        //        break;
        //    case 1:
        //        ShopManager.Instance.BuyRainbow();
        //        break;
        //}
        Debug.Log($"Has comprado el item con id {id}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (id)
        {
            case 0:
                shopperTxt.text = $"{ShopManager.Instance.shieldCost} Quesitos";
                break;
            case 1:

                shopperTxt.text = $"{ShopManager.Instance.rainbowCost} Quesitos";
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shopperTxt.text = "Welcome to the shop";
    }
}
