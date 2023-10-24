using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float jumpForce = 5.0f;
    private Rigidbody rb;

    [SerializeField] int desiredPath = 1;
    [SerializeField] int pathDistance = 5;
    private float targetXPosition = 0;

    public bool isMoving;
    public float groundedY;

    public float moveSpeed = 5f;


    public int currentPath = 2; // 1 for left, 2 for middle, 3 for right

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) && !isMoving)
        {
            desiredPath++;
            if (desiredPath >= 3) desiredPath = 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && !isMoving)
        {
            desiredPath--;
            if (desiredPath <= -1) desiredPath = 0;
        }

        if (desiredPath == 0)
        {
            targetXPosition = -8.5f;
            StartCoroutine(MoveToTargetPosition());
        }
        if (desiredPath == 1)
        {
            targetXPosition = 0;
            StartCoroutine(MoveToTargetPosition());
        }
        if (desiredPath == 2)
        {
            targetXPosition = 8.5f;
            StartCoroutine(MoveToTargetPosition());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (transform.position.y == groundedY) Jump();
        }
    }

    private void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("ObstacleGeneric"))
        {
            Time.timeScale = 0f;
        }
    
    }

    private IEnumerator MoveToTargetPosition()
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(targetXPosition, transform.position.y, transform.position.z);

        float journeyLenght = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float journeyFraction = distanceCovered / journeyLenght;

            transform.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);
            yield return null;
        }

        isMoving = false;
    }
}
