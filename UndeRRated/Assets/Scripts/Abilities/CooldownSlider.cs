using UnityEngine;
using UnityEngine.UI;

public class CooldownSlider : MonoBehaviour
{
    [SerializeField] private Slider cdSlider;

    public void UpdateSliderCooldown(float currentValue, float maxValue)
    {
        cdSlider.value = currentValue / maxValue;
    }
}

