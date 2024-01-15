using System.Collections;
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
            SoundManager.Instance.PlaySound(Audios.BatDie);
            Score.ExtraPoints();
            ParticleSystem particles = other.gameObject.GetComponentInChildren<ParticleSystem>();
            //particles.Play();
            Kill(other.gameObject);
            Kill(gameObject);
        }
    }

    // Disable the rat bullet after a determined time
    IEnumerator DisableAfterLifetime()
    {
        yield return new WaitForSeconds(bulletLifetime);
        gameObject.SetActive(false);
    }

    protected void Kill(GameObject obj)
    {
        obj.SetActive(false);
    }
}