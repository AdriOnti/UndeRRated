using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource effectManager;

    [SerializeField] private AudioClip energyField_SFX;
    [SerializeField] private AudioClip eatCheese_SFX;
    [SerializeField] private AudioClip ratDamage_SFX;

    // Start is called before the first frame update
    void Awake()
    {
        effectManager = GetComponent<AudioSource>();    
    }

    private void OnEnable()
    {
        RatController.RatAteCheese += EatCheeseSFX;
        RatController.RatTookDamage += RatTookDamageSFX;
        ProtectionField.ActivateShield += ActivatedShieldSFX;
    }
    private void EatCheeseSFX()
    {
        effectManager.PlayOneShot(eatCheese_SFX);
    }
    private void RatTookDamageSFX()
    {
        effectManager.PlayOneShot(ratDamage_SFX);
    }
    private void ActivatedShieldSFX()
    {
        effectManager.PlayOneShot(energyField_SFX);
    }
}
