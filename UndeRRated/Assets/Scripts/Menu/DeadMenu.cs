using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeadMenu : InGameMenu
{
    public static DeadMenu instance;

    private void Awake()
    {
        canvas = GameManager.Instance.GetUI();

        List<TextMeshProUGUI> list = GameManager.Instance.ResumedAndPausedAssets("DeadMenu");

        resumedScore = list[0];
        resumedCheese = list[1];
        pausedScore = list[2];
        pausedCheese = list[3];
        highscore = list[4];
        savedCheese = list[5];
        isDeadMenu = true;
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
        int tmp = Convert.ToInt32(resumedCheese.text)/* + GameManager.Instance.cheeseSaved*/;
        pausedCheese.text = $"Quesitos: {tmp}";
        GameManager.Instance.SaveMoney(Convert.ToInt32(resumedCheese.text), false);

        //string[] splitScore = pausedScore.GetParsedText().Split(' ');
        GameManager.Instance.SaveHighScore(Convert.ToInt32(pausedScore.text));
        highscore.text = $"HighScore: {GameManager.Instance.highScore}";

        GameManager.Instance.GetSavedMoney();
        savedCheese.text = $"Saved Quesitos: {GameManager.Instance.cheeseSaved}";

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
    /// 
    /// ¡¡¡¡¡¡¡ATENCIÓN!!!!!!
    /// REVIVIR FUNCIONA DE ESTA FORMA:
    /// 0.- Comprueba si hay la misma o más quesitos guardados que los requeridos para respawn
    /// 1.- Los quesitos guardados les resta el costo de respawn (variable del GameManager)
    /// 2.- Se le suma los quesitos recogidos durante la partida.
    /// En los menus se ven los quesitos guardados, puede parecer raro, lo de respawnear.
    /// 
    /// Olav: Dejo esto anotado, porque estoy 100% de que me olvidare de como funciona el respawn del juego.
    /// 
    /// Ejemplo: RespawnCost = 50;
    /// savedCheese = 50
    /// collectedCheese = 50;
    /// Resultado: 50 quesitos guardados
    /// </summary>
    //public void Respawn()
    //{
    //    SoundManager.Instance.PlayEffect(Audios.RatRespawn_1);
    //    Resume();
    //    RatController.Instance.isDead = false;

    //    if (GameManager.Instance.cheeseSaved >= GameManager.Instance.GetRespawnCost())
    //    {
    //        Debug.LogWarning($"Se han quitado {GameManager.Instance.GetRespawnCost()} quesitos de los {GameManager.Instance.cheeseSaved} guardados");

    //        GameManager.Instance.SaveMoney(Convert.ToInt32(resumedCheese.text), true);
    //        GameManager.Instance.GetSavedMoney();

    //    }
    //  //  else //{ //Debug.LogError($"Tu numero de quesitos es inferior a {GameManager.Instance.GetRespawnCost()}"); }

    //}
    public void Respawn()
    {
        RatController.Instance.isDead = false;
      

        if (GameManager.Instance.cheeseSaved >= GameManager.Instance.GetRespawnCost())
        {
            Debug.LogWarning($"Se han quitado {GameManager.Instance.GetRespawnCost()} quesitos de los {GameManager.Instance.cheeseSaved} guardados");

            //GameManager.Instance.SaveMoney(Convert.ToInt32(resumedCheese.text), true);
            GameManager.Instance.Respawn();
            GameManager.Instance.GetSavedMoney();
            Resume();
            SoundManager.Instance.PlayEffect(Audios.RatRespawn_1);
        }
        else { Debug.LogError($"Tu numero de quesitos es inferior a {GameManager.Instance.GetRespawnCost()}"); }

    }

}
