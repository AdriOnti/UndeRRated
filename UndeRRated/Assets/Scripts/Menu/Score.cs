using System;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI cheese;
    static float scoreAmount;
    static int cheeseAmount;
    public int pointIncreastedPerSec;
    static int killPoints = 10;
    protected bool enemyKilled;

    // Start is called before the first frame update
    void Start()
    {
        cheeseAmount = 0;
        scoreAmount = 0;
        pointIncreastedPerSec = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score.text = Convert.ToInt32(scoreAmount).ToString();
        scoreAmount += pointIncreastedPerSec * Time.deltaTime * 2;
        cheese.text = cheeseAmount.ToString();

        if (scoreAmount >= 100 && !GameManager.Instance.achievements[0])
        {
            GameManager.Instance.achievements[0] = true;
        }

        if (scoreAmount >= 500 && !GameManager.Instance.achievements[1])
        {
            GameManager.Instance.achievements[1] = true;
        }

        if (scoreAmount >= 1000 && !GameManager.Instance.achievements[2])
        {
            GameManager.Instance.achievements[2] = true;
        }
    }
    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    scoreAmount += ExtraPoints();
        //    enemyKilled = false;    
        //}
    }
    public static void ExtraPoints()
    {
        scoreAmount += killPoints;
    }

    public static void AddCheese(int points)
    {
        cheeseAmount += points;
    }
}
