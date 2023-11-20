using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class ObjectsPool : MonoBehaviour
{
    // Static Reference
    public static ObjectsPool instance;

    //Poison Balls
    private List<GameObject> pooledPoisonBalls = new List<GameObject>();
    private int amountOfPoisonBalls = 6;
    [SerializeField] private GameObject poisonBall;

    //Obstacles
    private List<GameObject> pooledObstacles = new List<GameObject>();
    private int amountOfObstaclesofTheSameType = 4;
    [SerializeField] public GameObject[] obstacles;

    System.Random rnd = new System.Random();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < amountOfPoisonBalls; i++)
        {
            GameObject obj = Instantiate(poisonBall);
            obj.SetActive(false);
            obj.transform.SetParent(instance.transform);
            pooledPoisonBalls.Add(obj);
        }
        for (int i = 0; i < obstacles.Length; i++)
        {
            for (int j = 0; j < amountOfObstaclesofTheSameType; j++)
            {
                GameObject obj = Instantiate(obstacles[i]);
                obj.SetActive(false);
                obj.transform.SetParent(instance.transform);
                pooledObstacles.Add(obj);
            }

        }

    }
    public GameObject GetPooledPoisonBall()
    {
        int rndNum = rnd.Next(0, pooledPoisonBalls.Count);

        if (!pooledPoisonBalls[rndNum].activeInHierarchy)
        {
            return pooledPoisonBalls[rndNum];
        }
        else return GetPooledPoisonBall();
    }

    public GameObject GetPooledObstacle()
    {
        Debug.Log(pooledObstacles.Count);
        //Debug.Log(obstacles.Length);
        int rndNum = rnd.Next(0, pooledObstacles.Count);

        if (!pooledObstacles[rndNum].activeInHierarchy)
        {
            Debug.Log(rndNum);
            return pooledObstacles[rndNum];
        }
        else return GetPooledObstacle();

    }

    /*public GameObject GetPooledRatBullet()
    {
        for (int i = 0; i < pooledPoisonBalls.Count; i++)
        {
            if (!pooledPoisonBalls[i].activeInHierarchy)
            {
                return pooledPoisonBalls[i];
            }
        }
        return null;
    }
    */
}
