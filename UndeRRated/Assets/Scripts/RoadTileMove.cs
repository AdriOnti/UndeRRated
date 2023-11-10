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
        //We move in Z direction at a constant speed the rigidbody of the road
        rb.transform.Translate(new Vector3(0,0,speed));
        
    }
    private void Update()
    {
        if ((Time.timeScale!=0)) Time.timeScale += 0.000005f;

    }
       

}
