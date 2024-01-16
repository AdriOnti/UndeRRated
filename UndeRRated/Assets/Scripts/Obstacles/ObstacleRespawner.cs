using System.Collections;
using UnityEngine;

public class ObstacleRespawner : MonoBehaviour
{
 
    public Transform objectPool;
    public static ObstacleRespawner Instance;
    public virtual void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Despawner"))
        {
            gameObject.transform.SetParent(objectPool);
            gameObject.SetActive(false);

        }
    }
}
