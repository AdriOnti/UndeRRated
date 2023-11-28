using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ObstacleRespawner
{
    private void Update()
    {
        //Debug.Log(parent);
    }
    private void Start()
    {
        objectPool = GameObject.FindGameObjectWithTag("Pool").transform;
    }
  
}
