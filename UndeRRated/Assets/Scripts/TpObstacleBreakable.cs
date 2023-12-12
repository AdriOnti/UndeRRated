using UnityEngine;

public class TpObstacleBreakable : MonoBehaviour
{
    public Transform teleportTarget;
	
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RatBullet"))
        {
            
        }
    }
}
