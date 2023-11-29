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
    public static float pausedTime;

    private void Start()
    {
        canvas = GameManager.Instance.GetUI();
    }

    private void Update()
    {
        pausedScore.text = $"Puntuación: {resumedScore.text}";
        pausedTime = Time.timeScale;
        if(GetComponent<Canvas>().enabled == true )
        {
            Debug.Log("Estoy pausado");
            Time.timeScale = 0;
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Resume();
            }
        }
        
    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main");
        
    }
}
