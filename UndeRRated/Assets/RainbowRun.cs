using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowRun : Ability
{

    private RainbowEffect rainbowEffect;
    private Light light;
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
        isInvincible = true;
        light.enabled = true;
        rainbowEffect.enabled = true;
        RatController.Instance.CallInvincibility(invincibleTime);
        saveTime = GameManager.Instance.ActualTime();
        Time.timeScale = 8f;
    }


    public void EndInvincibleTime()
    {
        isInvincible = false;
        rainbowEffect.enabled = false;
        light.enabled = false;
        Time.timeScale = saveTime;
 
        CooldownManager.Instance.PutOnCooldown(this);
        RatController.Instance.CallInvincibility(1f);
    }


}
