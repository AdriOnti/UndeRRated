using UnityEngine;

public class RoadTileMove : MonoBehaviour
{

    private Rigidbody rb;
    public static float speed = -1;
    public static bool deadRat;

 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Movemos la road en el eje Z constantemente
        rb.transform.Translate(new Vector3(0, 0, speed));
        
    }
    private void Update()
    {
        if (Time.timeScale!=0 && !deadRat && !(Time.timeScale > 2f)) Time.timeScale += 0.000005f;
    }       

}
