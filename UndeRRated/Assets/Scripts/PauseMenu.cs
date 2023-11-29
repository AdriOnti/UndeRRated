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
        pausedScore.text = $"Puntuaci�n: {resumedScore.text}";
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main");
        
    }
}
