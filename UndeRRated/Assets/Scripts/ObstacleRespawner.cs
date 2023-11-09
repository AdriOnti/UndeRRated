
using UnityEngine;

public class ObstacleRespawner : MonoBehaviour
{
 
    public Transform parent;
    public static ObstacleRespawner Instance;
    public virtual void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Despawner"))
        {
            gameObject.transform.SetParent(parent);
            gameObject.SetActive(false);
        }

    }

}
