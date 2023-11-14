using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBullet : ObstacleRespawner
{
    public float bulletLifetime = 10f;
    //private Vector3 targetPosition;
    //private float bulletSpeed;

    //public void StartMovement(Vector3 target, float speed)
    //{
    //    targetPosition = target;
    //    bulletSpeed = speed;
    //}

    //void Update()
    //{
    //    transform.position = Vector3.Lerp(transform.position, targetPosition, bulletSpeed * Time.deltaTime);
    //}

    void OnEnable()
    {
        // Inicia un temporizador para desactivar la bala después de cierto tiempo
        StartCoroutine(DisableAfterLifetime());
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Bat") /*|| collision.CompareTag("BrekeableObstacle")*/)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator DisableAfterLifetime()
    {
        yield return new WaitForSeconds(bulletLifetime);
        // Desactivar la bala después de su tiempo de vida
        gameObject.SetActive(false);
    }
}
