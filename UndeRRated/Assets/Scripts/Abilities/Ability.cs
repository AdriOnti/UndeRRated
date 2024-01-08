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


    public CooldownSlider slider;

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
            ratInputs.InGame.ForceField.performed += StartAbility;
        if (abilityName == "RainbowRun")
            ratInputs.InGame.RainbowRun.performed += StartAbility;
        if (abilityName == "Ratatata")
            ratInputs.InGame.Shot.performed += StartAbility;
    }

    protected virtual void StartAbility(InputAction.CallbackContext context)
    {
        if (CooldownManager.Instance.IsOnCooldown(this)) { return; }
     
        Cast();

        if (abilityName != "ForceField" && abilityName != "RainbowRun")
            CooldownManager.Instance.PutOnCooldown(this);
    }

    public abstract void Cast();

}