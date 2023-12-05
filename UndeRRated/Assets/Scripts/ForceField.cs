using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public Material[] materials;
    new Renderer renderer;
    Material blueMat;
    Material redMat;

    public static ForceField Instance;


    public bool isActive;

    private void Awake()
    {
        isActive = true;
        Instance = this;
    }
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        blueMat = materials[0];
        redMat = materials[1];
        renderer.sharedMaterial = blueMat;
    }


    public void Protect()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            StartCoroutine(Disapear());
        }
        StartCoroutine(DisableShield());
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
        this.gameObject.SetActive(false);
      
    }

}
