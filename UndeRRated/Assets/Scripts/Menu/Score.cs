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
    static int killCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        cheeseAmount = 0;
        scoreAmount = 0;
        pointIncreastedPerSec = 1;
    }

    // Aumento de puntos y comprovación de achievements
    void FixedUpdate()
    {
        score.text = Convert.ToInt32(scoreAmount).ToString();
        scoreAmount += pointIncreastedPerSec * Time.deltaTime * 2;
        cheese.text = cheeseAmount.ToString();

        if (scoreAmount >= 100 && !GameManager.Instance.achievementsBool[0])
        {
            GameManager.Instance.achievementsBool[0] = true;
            GameManager.Instance.ShowAchievement(0);
        }

        if (scoreAmount >= 500 && !GameManager.Instance.achievementsBool[1])
        {
            GameManager.Instance.achievementsBool[1] = true;
            GameManager.Instance.ShowAchievement(1);
        }

        if (scoreAmount >= 1000 && !GameManager.Instance.achievementsBool[2])
        {
            GameManager.Instance.achievementsBool[2] = true;
            GameManager.Instance.ShowAchievement(2);
        }

        if (scoreAmount >= 2147483646 && !GameManager.Instance.achievementsBool[2])
        {
            GameManager.Instance.achievementsBool[4] = true;
            GameManager.Instance.ShowAchievement(4);
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
        killCount++;
        if (killCount >= 100 && !GameManager.Instance.achievementsBool[3])
        {
            GameManager.Instance.achievementsBool[3] = true;
            GameManager.Instance.ShowAchievement(3);
        }
    }

    public static void AddCheese(int points)
    {
        cheeseAmount += points;
    }
}
