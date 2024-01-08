using System;
using UnityEngine;

public class DeadMenu : InGameMenu
{
    public static DeadMenu instance;

    private void Awake()
    {
        canvas = GameManager.Instance.GetUI();
    }

    /// <summary>
    /// Anectoda sobre este script.
    /// <para>Cuando se empezaba el menu de pausa, al morir por primera todo iba bien, 
    /// pero si revivias y volvias a morir podias darle al ESC y se abria el menu de pausa, 
    /// por lo que si le volvia a dar al ESC podia revivir sin gastar dinero </para>
    /// </summary>
    private void Update()
    {
        pausedScore.text = $"{resumedScore.text}";
        int tmp = Convert.ToInt32(resumedCheese.text) + GameManager.Instance.cheeseSaved;
        pausedCheese.text = $"Quesitos: {tmp}";
        GameManager.Instance.SaveMoney(Convert.ToInt32(resumedCheese.text));

        //string[] splitScore = pausedScore.GetParsedText().Split(' ');
        //Debug.Log(splitScore[0]);
        GameManager.Instance.SaveHighScore(Convert.ToInt32(pausedScore.text));

        if (GetComponent<Canvas>().enabled == true)
        {
            RoadTileMove.deadRat = true;
        }
    }

    // RESPAWN FUNCTION
    /// <summary>
    /// FUNCION EN PROCESO.
    /// Cuando este funcional, a cambio de una cantidad de monedas podras revivir, 
    /// para entonces esto estara en la escena UndeRRated como un canvas y no en otra escena
    /// </summary>
    public void Respawn()
    {
        //Debug.LogWarning("FALTA AÑADIRLE EL COSTE");
        //Debug.LogWarning("Se han quitado 50 quesitos");
        
        GameManager.Instance.SaveMoney(Convert.ToInt32(resumedCheese.text) - 50);
        Resume();
    }
}
