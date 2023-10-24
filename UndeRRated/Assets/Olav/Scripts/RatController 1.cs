using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using System.Timers;
using System;

public class RatController1 : MonoBehaviour
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

    private float targetXPosition = 0;
    private float targetYPosition = 5.13f;

    public bool isMoving;
    public bool isSliding;
    public bool isJumping;

    public float delta = 1.5f;  // Amount to move left and right from the start point
    public float speed = 2.0f;
    private Vector3 startPos;

    // METHODS
    void Start() 
    { 
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        startPos = transform.position;
    }


    // TO MOVE
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && !isSliding && !isMoving && !isJumping) 
        {
            targetYPosition += jumpForce;
            StartCoroutine(Jump());

            
        }

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
    }

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

    // SMOOTH MOVE IN THE X AXIS
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

    // TO JUMP
    private IEnumerator Jump()
    {
        if (isSliding == false)
        {
            isJumping = true;
            Vector3 startPos = transform.position;
            Vector3 targetPos = new Vector3(transform.position.x, targetYPosition, transform.position.z);

            float journeyLenght = Vector3.Distance(startPos, targetPos);
            float startTime = Time.time;

            while (transform.position != targetPos)
            {
                float distanceCovered = (Time.time - startTime) * moveSpeed;
                float journeyFraction = distanceCovered / journeyLenght;

                transform.position = Vector3.Lerp(startPos, targetPos, journeyFraction);
                yield return null;
            }            

            targetYPosition -= jumpForce;
            StartCoroutine(Slide());

            isJumping = false;
        }
    }
}
