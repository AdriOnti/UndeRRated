using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float jumpForce = 5.0f;
    private Rigidbody rb;

    public float moveSpeed = 10.0f;
    public Vector3 targetPosition;


    public int currentPath = 2; // 1 for left, 2 for middle, 3 for right

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentPath += 1;
            MoveToPath(currentPath); // Move 1 to the right
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentPath -= 1;
            MoveToPath(currentPath); // Move to the left 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void MoveToPath(int path)
    {
        float step = moveSpeed * Time.deltaTime;


        switch (path)
        {
            case 1: // Left (path 1)
                targetPosition = new Vector3(-6.0f, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, step);
                break;
            case 2: // Middle (path 2)
                targetPosition = new Vector3(0.0f, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, step);
                break;
            case 3: // Right (path 3)
                targetPosition = new Vector3(6.0f, transform.position.y, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, targetPosition, step);
                break;
        }

    }
    private void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
