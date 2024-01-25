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

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.GetSavedMoney();
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
    /// A cambio de una determinada cantidad de quesitos se te permite revivir.
    /// </summary>
    /* 
     * ¡¡¡¡¡¡¡ATENCIÓN!!!!!!
     * REVIVIR FUNCIONA DE ESTA FORMA:
     * 0.- Comprueba si hay la misma o más quesitos guardados que los requeridos para respawn
     * 1.- Si los hay directamente te quita lo que cueste el RespawnCost del GameManager.2.- Finalmente te revive
     * 
     * Esto tuvo multiples versiones pero parece que esta es la buena
    */
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
