using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    // RESPAWN FUNCTION
    /// <summary>
    /// FUNCION EN PROCESO.
    /// Cuando este funcional, a cambio de una cantidad de monedas podras revivir, 
    /// para entonces esto estara en la escena UndeRRated como un canvas y no en otra escena
    /// </summary>
    public void Respawn()
    {
        Debug.Log("EN MANTENIMIENTO");
        Debug.LogWarning("MANTENIMENT IN PROCESS");
    }

    // RESTART FUNCTION
    /// <summary>
    /// Carga la escena UndeRRated para comenzar una nueva partida
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene("UndeRRated");
    }
}
