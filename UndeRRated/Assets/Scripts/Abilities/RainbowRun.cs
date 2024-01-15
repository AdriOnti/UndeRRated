using UnityEngine;

public class RainbowRun : Ability
{

    private RainbowEffect rainbowEffect;
    private new Light light;
    public static RainbowRun Instance;
    private float saveTime;
    public float invincibleTime;
    public bool isInvincible;

    protected override void Awake()
    {
        Instance = this;
        rainbowEffect = GetComponentInChildren<RainbowEffect>();
        light = GetComponentInChildren<Light>();
        base.Awake();
    }
    public override void Cast()
    {
        if (isInvincible || RatController.Instance.ratInvincible) return;
        isInvincible = true;
        light.enabled = true;
        rainbowEffect.enabled = true;
        RatController.Instance.CallInvincibility(invincibleTime);
        SoundManager.Instance.PlaySound(Audios.AbilityStar);
        saveTime = Time.timeScale;
        Time.timeScale = 8f;
    }


    public void EndInvincibleTime()
    {
        CooldownManager.Instance.PutOnCooldown(this);
        isInvincible = false;
        rainbowEffect.enabled = false;
        light.enabled = false;
        Time.timeScale = saveTime;
        RatController.Instance.CallInvincibility(1f);
       
    }


}
