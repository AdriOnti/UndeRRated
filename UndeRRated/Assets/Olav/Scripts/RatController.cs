using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class RatController : MonoBehaviour
{
    [SerializeField] int desiredPath = 1;
    private float targetXPosition = 0;
    public float pathDistance = 7.0f;
    public float moveSpeed = 30f;
    private Rigidbody rb; 
    public float jumpForce = 5.0f;
    public bool isMoving;
    public bool isJumping;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.UpArrow) && !isMoving && !isJumping)
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && !isMoving && !isJumping)
        {
            desiredPath++;
            if (desiredPath >= 3) desiredPath = 2;
        } 
        else if(Input.GetKeyUp(KeyCode.LeftArrow) && !isMoving && !isJumping)
        {
            desiredPath--;
            if (desiredPath <= -1) desiredPath = 0;
        }

        GoToPath();
    }

    private void Jump()
    {
        isJumping = true;
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //yield return null;
        }
        isJumping = false;
    }

    private void GoToPath()
    {
        if (desiredPath == 0) targetXPosition = pathDistance * -1;
        if (desiredPath == 1) targetXPosition = 0;
        if (desiredPath == 2) targetXPosition = pathDistance;
        StartCoroutine(MoveToTargetPosition());
    }

    private IEnumerator MoveToTargetPosition()
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(targetXPosition, transform.position.y, transform.position.z);

        float journeyLenght = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while(transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float journeyFraction = distanceCovered / journeyLenght;

            transform.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);
            yield return null;
        }

        isMoving = false;
    }
}
