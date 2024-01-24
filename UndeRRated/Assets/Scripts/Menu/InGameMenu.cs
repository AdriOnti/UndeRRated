using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : Menu
{
    protected TextMeshProUGUI pausedScore;
    protected TextMeshProUGUI resumedScore;
    protected TextMeshProUGUI resumedCheese;
    protected TextMeshProUGUI pausedCheese;

    protected TextMeshProUGUI highscore;
    protected TextMeshProUGUI savedCheese;
    protected bool isDeadMenu;

    public void ReturnMainMenu()
    {
        StartCoroutine(BackMain());
    }

    IEnumerator BackMain()
    {
        if (isDeadMenu)
        {
            string[] splitCheese = pausedCheese.GetParsedText().Split(' ');
            GameManager.Instance.SaveMoney(Convert.ToInt32(splitCheese[1]), false);
            //string[] splitScore = pausedScore.GetParsedText().Split(' ');
            GameManager.Instance.SaveHighScore(Convert.ToInt32(pausedScore.text));

            yield return new WaitForSeconds(0.5f);
        }
        Time.timeScale = 1.0f;
        RoadTileMove.speed = -1;
        SceneManager.LoadScene("Main");

    }

}