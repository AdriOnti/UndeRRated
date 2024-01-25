using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class PauseMenu : InGameMenu
{
    public static float pausedTime;
    private void Awake()
    {
        canvas = GameManager.Instance.GetUI();
        List<TextMeshProUGUI> list = GameManager.Instance.ResumedAndPausedAssets("PauseMenu");

        resumedScore = list[0];
        resumedCheese = list[1];
        pausedScore = list[2];
        pausedCheese = list[3];
        highscore = list[4];
        savedCheese = list[5];
    }

    //private void OnEnable()
    //{
    //    GameManager.Instance.GetSavedMoney();
    //}

    private void Update()
    {
        pausedScore.text = $"Score: {resumedScore.text}";
        int tmp = Convert.ToInt32(resumedCheese.text) /*+ GameManager.Instance.cheeseSaved*/;
        pausedCheese.text = $"Quesitos: {tmp}";
        GameManager.Instance.SaveMoney(Convert.ToInt32(resumedCheese.text), false);

        string[] splitScore = pausedScore.GetParsedText().Split(' ');
        GameManager.Instance.SaveHighScore(Convert.ToInt32(splitScore[1]));
        highscore.text = $"HighScore: {GameManager.Instance.highScore}";

        GameManager.Instance.GetSavedMoney();
        savedCheese.text = $"Saved Quesitos: {GameManager.Instance.cheeseSaved}";

        pausedTime = Time.timeScale;
        if (GetComponent<Canvas>().enabled == true)
        {
           

            Time.timeScale = 0;
            if (Input.GetKeyUp(KeyCode.Escape))
            {             
                Resume();
            }
        }
    }
}