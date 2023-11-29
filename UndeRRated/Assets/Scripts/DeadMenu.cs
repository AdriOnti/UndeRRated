using UnityEngine;

public class DeadMenu : Menu
{
    public static DeadMenu instance;

    /// <summary>
    /// Anectoda sobre este script.
    /// <para>Cuando se empezaba el menu de pausa, al morir por primera todo iba bien, 
    /// pero si revivias y volvias a morir podias darle al ESC y se abria el menu de pausa, 
    /// por lo que si le volvia a dar al ESC podia revivir sin gastar dinero </para>
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.LogWarning("NO PUEDES PAUSAR *golpea el baston en el suelo*");
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
        
        Resume();
    }
}
