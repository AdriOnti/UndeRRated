using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RatController : MonoBehaviour
{
    // PARAMETERS
    private CharacterController controller;
    private Vector3 direction;

    private int desiredPath = 1;    // 0: Left Path, 1: Middle Path, 2: Right Path
    public float pathDistance = 4;

    public float moveSpeed;

    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;
    public bool isSliding;

    private float targetXPosition = 0;

    public bool isMoving = false;

    // METHODS
    void Start() 
    { 
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    // TO MOVE
    void Update()
    {
        // if the character is in the ground, you can jump
        if (controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKey(KeyCode.UpArrow)) { Jump(); }
        }
        else { direction.y += Gravity * Time.deltaTime; }

        if (Input.GetKeyUp(KeyCode.DownArrow) && !isSliding) { StartCoroutine(Slide()); }



        if (Input.GetKeyUp(KeyCode.RightArrow) && !isMoving)
        {
            // Move to the right path
            desiredPath++;
            if (desiredPath >= 3) desiredPath = 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && !isMoving)
        {
            // Move to the left path
            desiredPath--;
            if (desiredPath <= -1) desiredPath = 0;
        }

        if (!isSliding)
        {
            if (desiredPath == 0)
            {
                targetXPosition = -5.0f;
                StartCoroutine(MoveToTargetPosition());
            }
            if (desiredPath == 1)
            {
                targetXPosition = 0f;
                StartCoroutine(MoveToTargetPosition());
            }
            if (desiredPath == 2)
            {
                targetXPosition = 5f;
                StartCoroutine(MoveToTargetPosition());
            }
        }

        Debug.Log("Mov: " + isMoving);
        Debug.Log("Slide: " + isSliding);
    }

    //private void FixedUpdate() { controller.Move(direction * Time.fixedDeltaTime); }

    // TO JUMP
    private void Jump() { direction.y = jumpForce; }

    // TO SLIDE
    private IEnumerator Slide()
    {
        // Slide turn on
        isSliding = true;

        if(isMoving == false) 
        {
            float slideDuration = 0.5f;
            float elapsedTime = 0;
            Vector3 startPos = transform.position;

            while (elapsedTime < slideDuration)
            {
                float slideAmount = Mathf.Lerp(0, -1.1f, elapsedTime / slideDuration);
                transform.position = new Vector3(transform.position.x, startPos.y + slideAmount, transform.position.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Slide turn off
            elapsedTime = 0;
            while (elapsedTime < slideDuration)
            {
                float slideAmount = Mathf.Lerp(0, 0.025f, elapsedTime / slideDuration);
                transform.position = new Vector3(transform.position.x, startPos.y + slideAmount, transform.position.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = new Vector3(transform.position.x, 5.13f, transform.position.z);

            isSliding = false;
        }
        
    }

    private IEnumerator MoveToTargetPosition()
    {
        if (isSliding == false)
        {
            isMoving = true;
            Vector3 startPos = transform.position;
            Vector3 targetPos = new Vector3(targetXPosition, transform.position.y, transform.position.z);

            float journeyLenght = Vector3.Distance(startPos, targetPos);
            float startTime = Time.time;

            while (transform.position != targetPos)
            {
                float distanceCovered = (Time.time - startTime) * moveSpeed;
                float journeyFraction = distanceCovered / journeyLenght;

                transform.position = Vector3.Lerp(startPos, targetPos, journeyFraction);
                yield return null;
            }

            isMoving = false;
        }
    }
}
