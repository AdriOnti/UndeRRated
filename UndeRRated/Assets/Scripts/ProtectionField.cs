using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProtectionField : Ability
{
    public Material[] materials;
    new Renderer renderer;
    Material blueMat;
    Material redMat;


    public float invincibleTime = 5f;
    public static ProtectionField Instance;
    public bool isActive;

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
    private void Start()
    {

    }


    public void Protect()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            StartCoroutine(Disapear());
        }
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
        yield return new WaitForSeconds(1.1f);
        isActive = false;
        renderer.enabled = false;   
      
    }
    public override void Cast()
    {
        if(isActive) return; 
          Debug.Log($"I am casting: {this.AbilityName}");
        isActive = true;
        renderer.enabled = true;
    }
}