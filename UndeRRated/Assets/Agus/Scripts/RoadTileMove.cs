using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTileMove : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.transform.Translate(new Vector3(0,0,speed));
    }

    //public void React()
    //{
    //    this.transform.position = teleportTarget.transform.position;
    //}
}
