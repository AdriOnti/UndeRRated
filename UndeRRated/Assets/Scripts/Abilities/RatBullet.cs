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
        if (other.CompareTag("Bat") || other.CompareTag("ObstacleBreakable"))
        {
            Score.ExtraPoints();
            ParticleSystem particles = other.gameObject.GetComponentInChildren<ParticleSystem>();
            //particles.Play();
            Die(other.gameObject);
            Die(gameObject);
        }
    }

    // Disable the rat bullet after a determined time
    IEnumerator DisableAfterLifetime()
    {
        yield return new WaitForSeconds(bulletLifetime);
        gameObject.SetActive(false);
    }

    public void Die(GameObject obj)
    {

        obj.SetActive(false);
    }
}
