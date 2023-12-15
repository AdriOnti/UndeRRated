using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PauseMenu : InGameMenu
{
    public static float pausedTime;

    private void Awake()
    {
        canvas = GameManager.Instance.GetUI();
    }

    private void Update()
    {
        pausedScore.text = $"Score: {resumedScore.text}";
        int tmp = Convert.ToInt32(resumedCheese.text) + GameManager.Instance.cheeseSaved;
        pausedCheese.text = $"Quesitos: {tmp}";
        GameManager.Instance.SaveMoney(Convert.ToInt32(resumedCheese.text));

        string[] splitScore = pausedScore.GetParsedText().Split(' ');
        GameManager.Instance.SaveHighScore(Convert.ToInt32(splitScore[1]));

        pausedTime = Time.timeScale;
        if(GetComponent<Canvas>().enabled == true )
        {
            Time.timeScale = 0;
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Resume();
            }
        }
    }
}
