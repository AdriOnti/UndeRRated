using System;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI cheese;
    public static float scoreAmount;
    public static int cheeseAmount;
    public int pointIncreastedPerSec;
    static int killPoints = 10;
    protected bool enemyKilled;
    static int killCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        cheeseAmount = 0;
        scoreAmount = 450;
        pointIncreastedPerSec = 1;
    }

    // Aumento de puntos y comprovación de achievements
    void FixedUpdate()
    {
        score.text = Convert.ToInt32(scoreAmount).ToString();
        scoreAmount += pointIncreastedPerSec * Time.deltaTime * 2;
        cheese.text = cheeseAmount.ToString();

        if (scoreAmount >= 100 && !GameManager.Instance.achievementsBool[1])
        {
            GameManager.Instance.achievementsBool[1] = true;
            GameManager.Instance.ShowAchievement(0);
            DataManager.instance.SaveAchievement(0);
        }

        if (scoreAmount >= 500 && !GameManager.Instance.achievementsBool[2])
        {
            GameManager.Instance.achievementsBool[2] = true;
            GameManager.Instance.ShowAchievement(1);
            DataManager.instance.SaveAchievement(1);
        }

        if (scoreAmount >= 1000 && !GameManager.Instance.achievementsBool[3])
        {
            GameManager.Instance.achievementsBool[3] = true;
            GameManager.Instance.ShowAchievement(2);
            DataManager.instance.SaveAchievement(2);
        }

        if (scoreAmount >= 2147483646 && !GameManager.Instance.achievementsBool[5])
        {
            GameManager.Instance.achievementsBool[5] = true;
            GameManager.Instance.ShowAchievement(4);
            DataManager.instance.SaveAchievement(4);
        }
    }
    public static void ExtraPoints()
    {
        
        scoreAmount += killPoints;
        killCount++;
        if (killCount >= 100 && !GameManager.Instance.achievementsBool[4])
        {
            GameManager.Instance.achievementsBool[4] = true;
            GameManager.Instance.ShowAchievement(3);
            DataManager.instance.SaveAchievement(3);
        }
    }

    public static void AddCheese(int points)
    {
        cheeseAmount += points;
        //GameManager.Instance.SaveMoney(points, false);
    }
}
