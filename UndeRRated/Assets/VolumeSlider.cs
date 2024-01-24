using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;


    private void Awake()
    {
       slider = GetComponentInChildren<Slider>();
    }

    
    private void UpdateSlider()
    {
        //AudioListener.volume = 
    }


}
