using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    public static CooldownManager Instance;
    [SerializeField] private List<CooldownData> abilitiesOnCooldown = new();

    [System.Serializable]
    private class CooldownData
    {
        public Ability ability;
        public float cooldown;
        public CooldownData(Ability ability, float cooldown)
        {
            this.ability = ability;
            this.cooldown = cooldown;
        }

    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PutOnCooldown(Ability ability)
    {
        CooldownData abilitycd = new(ability, ability.AbilityCooldown);

        abilitiesOnCooldown.Add(abilitycd);
        StartCoroutine(Cooldown(abilitycd, ability));
    }


    IEnumerator Cooldown(CooldownData CDability, Ability ability)
    {
        float maxCooldown = CDability.cooldown;
        while (CDability.cooldown > 0f)
        {
            ability.slider.UpdateSliderCooldown(CDability.cooldown, maxCooldown);
            if(!GameManager.Instance.stopCooldowns) CDability.cooldown -= Time.deltaTime;
            yield return null;
        }
        abilitiesOnCooldown.Remove(CDability);
        if (ability.AbilityName != "Ratatata") SoundManager.Instance.PlayEffect(Audios.AbilityCooldownEnd);
    }
    public bool IsOnCooldown(Ability ability)
    {
        foreach (CooldownData cooldownData in abilitiesOnCooldown)
        {
            if (cooldownData.ability == ability)
            {
               
                return true;
            }

        }
        return false;
    }

}
