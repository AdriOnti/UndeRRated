using System;
using System.Collections;
using UnityEngine;

public class ProtectionField : Ability
{
    public Material[] materials;
    new Renderer renderer;
    Material blueMat;
    Material redMat;


    public float invincibleTime = 2f;
    public static ProtectionField Instance;
    public bool isActive;

    // Sound events
    public static Action ActivateShield;

    protected override void Awake()
    {
        base.Awake();
        renderer = GetComponent<Renderer>();
        isActive = false;
        Instance = this;

        blueMat = materials[0];
        redMat = materials[1];
        renderer.sharedMaterial = blueMat;
        renderer.enabled = false;
    }
    public void Protect()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            StartCoroutine(Disapear());
        }
        SoundManager.Instance.PlaySound(Audios.AbilityShieldBreak);
        StartCoroutine(DisableShield());
        CooldownManager.Instance.PutOnCooldown(this);
    }

    IEnumerator Disapear()
    {
        renderer.sharedMaterial = redMat;
        yield return new WaitForSeconds(1f);
        renderer.sharedMaterial = blueMat;
    }
    IEnumerator DisableShield()
    {
        SoundManager.Instance.PlaySound(Audios.AbilityShieldDisable);
        yield return new WaitForSeconds(1.1f);
        isActive = false;
        renderer.enabled = false;   
      
    }
    public override void Cast()
    {
        if(isActive) return; 
        isActive = true;
        renderer.enabled = true;
        ActivateShield?.Invoke();
    }
}
