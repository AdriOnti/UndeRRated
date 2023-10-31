using UnityEngine;

public class TpObstacleBreakable : MonoBehaviour
{
    public Transform teleportTarget;
	
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "playerTest")
        {
            transform.position = teleportTarget.transform.position;
        }
    }
}
