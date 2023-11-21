using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    public TextMeshProUGUI pausedScore;
    public TextMeshProUGUI resumedScore;


    private void Update()
    {
        pausedScore.text = $"Puntuación: {resumedScore.text}";
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
