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
            particles.Play();
            StartCoroutine(Die(other.gameObject));
            StartCoroutine(Die(gameObject));
        }
    }

    // Disable the rat bullet after a determined time
    IEnumerator DisableAfterLifetime()
    {
        yield return new WaitForSeconds(bulletLifetime);
        gameObject.SetActive(false);
    }

    public IEnumerator Die(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }
}
