using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    // Static Reference
    public static ObjectsPool instance;

    // Poison Balls
    private List<GameObject> pooledPoisonBalls = new List<GameObject>();
    private int amountOfPoisonBalls = 6;
    [SerializeField] private GameObject poisonBall;

    // Obstacles
    private List<GameObject> pooledObstacles = new List<GameObject>();
    private int amountOfObstaclesofTheSameType = 4;
    [SerializeField] public GameObject[] obstacles;

    // Rat Bullets
    private List<GameObject> pooledRatBullets = new List<GameObject>();
    private int amountOfRatBullets = 15;
    [SerializeField] public GameObject ratBullet;

    // Cheese
    private List<GameObject> pooledCheese = new List<GameObject>();
    private int amountOfCheese = 30;
    [SerializeField] public GameObject cheese;

    // Rotten Cheese
    private int amountOfRottenCheese = 2;
    public GameObject rottenCheese;

    // Mega Cheese
    private List<GameObject> pooledMegaCheese = new List<GameObject>();
    private int amountOfMegaCheese = 3;
    public GameObject megaCheese;

    // Random Number
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
        // POISION BALLS
        for (int i = 0; i < amountOfPoisonBalls; i++)
        {
            GameObject obj = Instantiate(poisonBall);
            obj.SetActive(false);
            obj.transform.SetParent(instance.transform);
            pooledPoisonBalls.Add(obj);
        }

        // OBSTACLES
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

        // RAT BULLETS
        for(int i = 0; i < amountOfRatBullets; i++)
        {
            GameObject obj = Instantiate(ratBullet);
            obj.SetActive(false);
            obj.transform.SetParent(instance.transform);
            pooledRatBullets.Add(obj);
        }

        // CHEEEESEEEE
        for (int i = 0; i < amountOfCheese; i++)
        {
            GameObject obj = Instantiate(cheese);
            obj.SetActive(false);
            obj.transform.SetParent(instance.transform);
            pooledCheese.Add(obj);
        }

        // ROTTEN CHEESE
        for (int i = 0; i < amountOfRottenCheese; i++)
        {
            GameObject obj = Instantiate(rottenCheese);
            obj.SetActive(false);
            obj.transform.SetParent(instance.transform);
            pooledCheese.Add(obj);
        }

        // MEGA CHEESE
        for (int i = 0; i < amountOfMegaCheese; i++)
        {
            GameObject obj = Instantiate(megaCheese);
            obj.SetActive(false);
            obj.transform.SetParent(instance.transform);
            pooledMegaCheese.Add(obj);
        }

    }

    /// <summary>
    /// Recursivamente busca una poison ball libre en la pool
    /// </summary>
    /// <returns>Poison Ball</returns>
    public GameObject GetPooledPoisonBall()
    {
        int rndNum = rnd.Next(0, pooledPoisonBalls.Count);

        if (!pooledPoisonBalls[rndNum].activeInHierarchy)
        {
            return pooledPoisonBalls[rndNum];
        }
        else return GetPooledPoisonBall();
    }

    /// <summary>
    /// Recursivamente busca un obstáculo libre en la pool
    /// </summary>
    /// <returns>Obstacle</returns>
    public GameObject GetPooledObstacle()
    {
        int rndNum = rnd.Next(0, pooledObstacles.Count);
        if (!pooledObstacles[rndNum].activeInHierarchy)
        {
            return pooledObstacles[rndNum];
        }
        else return GetPooledObstacle();

    }

    /// <summary>
    /// Recursivamente busca una bullet libre en la pool
    /// </summary>
    /// <returns>Rat bullet</returns>
    public GameObject GetPooledRatBullet()
    {
        int rndNum = rnd.Next(0, pooledRatBullets.Count);

        if (!pooledRatBullets[rndNum].activeInHierarchy)
        {
            return pooledRatBullets[rndNum];
        }
        else return GetPooledRatBullet();
    }

    /// <summary>
    /// Recursivamente busca un quesito libre en la pool
    /// </summary>
    /// <returns>Quesito</returns>
    public GameObject GetPooledCheese()
    {
        int rndNum = rnd.Next(0, pooledCheese.Count);

        if (!pooledCheese[rndNum].activeInHierarchy)
        {
            return pooledCheese[rndNum];
        }
        else return GetPooledCheese();
    }

    /// <summary>
    /// Recursivamente busca un mega quesito libre en la pool
    /// </summary>
    /// <returns>Mega quesito</returns>
    public GameObject GetPooledMegaCheese()
    {
        int rndNum = rnd.Next(0, pooledMegaCheese.Count);

        if (!pooledMegaCheese[rndNum].activeInHierarchy)
        {
            return pooledMegaCheese[rndNum];
        }
        else return GetPooledMegaCheese();
    }
}
