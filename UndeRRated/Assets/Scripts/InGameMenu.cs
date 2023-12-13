using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : Menu
{
    public TextMeshProUGUI pausedScore;
    public TextMeshProUGUI resumedScore;
    public TextMeshProUGUI resumedCheese;
    public TextMeshProUGUI pausedCheese;
    public bool isDeadMenu;

    public void ReturnMainMenu()
    {
        
        StartCoroutine(BackMain());
    }

    IEnumerator BackMain()
    {
        if (isDeadMenu)
        {
            string[] splitCheese = pausedCheese.GetParsedText().Split(' ');
            GameManager.Instance.SaveMoney(Convert.ToInt32(splitCheese[1])); 
            string[] splitScore = pausedScore.GetParsedText().Split(' ');
            GameManager.Instance.SaveHighScore(Convert.ToInt32(splitScore[1]));

            yield return new WaitForSeconds(2f);
        }
        else
        {
            Debug.LogWarning("Ni tu score ni los quesitos recorrelctados se guardaran");
        }
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main");

    }
}
