using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private string abilityName = "New Ability Name";

    [SerializeField] private string abilityDescription = "New Ability Description";

    [SerializeField] private float abilityCooldown = 1f;

    
    public float AbilityCooldown { get { return abilityCooldown; } }

    public string AbilityDescription { get { return abilityDescription; } }
    public string AbilityName { get { return abilityName; } }

    private RatInputs ratInputs;

    private void OnEnable()
    {
        ratInputs.Enable();
    }
    private void OnDisable()
    {
        ratInputs.Disable();
    }
    protected virtual void Awake()
    {
        ratInputs = new RatInputs();

        if (abilityName == "ForceField")
            ratInputs.InGame.ForceField.performed += Attack;

        if (abilityName == "InmunityBoost")
            ratInputs.InGame.InmunityBoost.performed += Attack;

    }

    protected virtual void Attack(InputAction.CallbackContext context)
    {
        if (CooldownManager.Instance.IsOnCooldown(this)) { return; }
        Debug.Log($"I am casting: {abilityName}");
        Cast();
        CooldownManager.Instance.PutOnCooldown(this);
    }

    public abstract void Cast();

}
