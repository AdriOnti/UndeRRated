using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBullet : ObstacleRespawner
{
    public float bulletLifetime;
    //public float bulletSpeed;

    void OnEnable() { StartCoroutine(DisableAfterLifetime()); }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Bat") /*|| collision.CompareTag("BrekeableObstacle")*/)
        {
            gameObject.SetActive(false);
        }
    }

    // Disable the rat bullet after a determined time
    IEnumerator DisableAfterLifetime()
    {
        yield return new WaitForSeconds(bulletLifetime);
        gameObject.SetActive(false);
    }
}
