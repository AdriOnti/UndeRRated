using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingBat : MonoBehaviour
{

    public GameObject projectile;
    public Transform parent;
    public Transform parentRoad;
    private bool isShooting;
    public Transform[] shootTargets = new Transform[3];
    private int targetIndex;
    private float speed = 150f;
    void Start()
    {
        projectile.transform.position = transform.position;
    }

    void Awake()
    {
        System.Random rnd = new System.Random();
        targetIndex = rnd.Next(shootTargets.Length);
    }

    private void Update()
    {
        if (isShooting)
        {
            projectile.transform.SetParent(parent);
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, shootTargets[targetIndex].position, speed * Time.deltaTime);
            if (Vector3.Distance(projectile.transform.position, shootTargets[targetIndex].position) < 0.01f)
            {
                isShooting = false;
                projectile.transform.SetParent(parentRoad);
            }
        }
          
        
        //if (Vector3.Distance(projectile.transform.position, shootTargets[targetIndex].position) < 0.01f)
        //{
        //    isMoving = false;
        //    transform.SetParent(parentRoad);
        //}




    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("AttackTrigger"))
        {
            Debug.Log("DISPARA");
            isShooting = true;
        }

    }
}
