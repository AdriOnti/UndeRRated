
using UnityEngine;

public class ObstacleRespawner : MonoBehaviour
// Start is called before the first frame update
{
 
    public Transform parent;
    public static ObstacleRespawner Instance;
    private void OnBecameInvisible()
    {
        enabled = false;
        this.gameObject.transform.SetParent(parent);
    }


}
