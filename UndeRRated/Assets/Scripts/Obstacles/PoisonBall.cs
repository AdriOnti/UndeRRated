using UnityEngine;

public class PoisonBall : ObstacleRespawner
{

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.gameObject.CompareTag("RatBullet") )
        {
            // Play Animation of explosion
            gameObject.SetActive(false);
        }
    }
}
