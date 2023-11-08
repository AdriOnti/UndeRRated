using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Transform parent;
    private void Update()
    {
        //Debug.Log(parent);
    }
    private void Start()
    {
        parent = GameObject.FindGameObjectWithTag("Pool").transform;
    }
    private void OnBecameInvisible()
    {
         try
        {
            this.gameObject.transform.SetParent(parent);
            this.gameObject.SetActive(false);
        }
        catch { }
    }
}
