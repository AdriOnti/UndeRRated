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

    // RESTART FUNCTION
    /// <summary>
    /// Carga la escena UndeRRated para comenzar una nueva partida
    /// </summary>
    public void Restart()
    {
        GameManager.Instance.SaveMoney(Convert.ToInt32(resumedCheese.text), false);
        Time.timeScale = 1f;
        RoadTileMove.speed = -1;
        if (SceneManager.GetActiveScene().name != "UndeRRated") SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else SceneManager.LoadScene("UndeRRated");
        Physics.IgnoreLayerCollision(6, 7, false);
    }

}