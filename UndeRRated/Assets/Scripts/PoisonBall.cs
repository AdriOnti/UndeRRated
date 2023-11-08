using UnityEditor;
using UnityEngine;

public class PoisonBall : ObstacleRespawner
{
    
    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
    //    {

         
    //    }
    //}
    private void OnBecameInvisible()
    {
        try
        {
            this.gameObject.transform.SetParent(parent);
            this.gameObject.SetActive(false);
        }
        catch { }
    }
}
