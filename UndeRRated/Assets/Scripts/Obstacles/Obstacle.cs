using UnityEngine;

public class Obstacle : ObstacleRespawner
{
    private void Start()
    {
        objectPool = GameObject.FindGameObjectWithTag("Pool").transform;
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Cheese") || other.gameObject.CompareTag("MegaCheese"))
        {
            if(name == "ObstacleS(Clone)")
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 2f, other.transform.position.z);
            }
            else if (name == "ObstacleN(Clone)" || name == "ObstacleBN(Clone)")
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 5f, other.transform.position.z);
            }
            else if (name == "ObstacleT(Clone)" || name == "ObstacleBT(Clone)")
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
