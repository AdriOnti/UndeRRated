using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.transform.Translate(new Vector3(0,0,speed));
    }
}
