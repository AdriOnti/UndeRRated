using UnityEditor;
using UnityEngine;

public class PoisonBall : ObstacleRespawner
{

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        //if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        //{


        //}

        if (collision.gameObject.CompareTag("RatBullet") )
        {
            // Play Animation of explosion
            gameObject.SetActive(false);
        }
    }
}
