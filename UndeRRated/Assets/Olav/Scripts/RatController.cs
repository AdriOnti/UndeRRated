using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RatController : MonoBehaviour
{
    // Mathf.Sin:
    //[SerializeField] Vector3 movementVector = new(10f, 0f, 0f);
    //[SerializeField] float period = 2f;
    //public bool isStop = false;
    //public float movementFactor;
    //Vector3 startingPos;

    [SerializeField] int desiredPath = 1;
    [SerializeField] int pathDistance = 5;
    private float targetXPosition = 0;

    public bool isMoving;

    public float moveSpeed = 5f;


    void Start()
    {
        //startingPos = transform.position; // Mathf.Sin
        //rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (period <= Mathf.Epsilon || isStop) { return; }

        //float cycles = Time.time / period;

        //const float tau = Mathf.PI * 2;
        //float rawSinWave = Mathf.Sin(-180f * tau);

        //movementFactor = rawSinWave;
        //Vector3 offset = movementVector * movementFactor;
        //transform.position = startingPos + offset;

        if (Input.GetKeyUp(KeyCode.RightArrow) && !isMoving)
        {
            desiredPath++;
            if (desiredPath >= 3) desiredPath = 2;
        } else if(Input.GetKeyUp(KeyCode.LeftArrow) && !isMoving)
        {
            desiredPath--;
            if (desiredPath <= -1) desiredPath = 0;
        }

        if(desiredPath == 0)
        {
            targetXPosition = -7.0f;
            StartCoroutine(MoveToTargetPosition());
        }
        if(desiredPath == 1)
        {
            targetXPosition = 0;
            StartCoroutine(MoveToTargetPosition());
        }
        if(desiredPath == 2)
        {
            targetXPosition = 7.0f;
            StartCoroutine(MoveToTargetPosition());
        }
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
