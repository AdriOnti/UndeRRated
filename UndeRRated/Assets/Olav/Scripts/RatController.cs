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
    private bool isSliding;

    private Vector3 targetPosition;

    // METHODS
    void Start() 
    { 
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        targetPosition = transform.position;
    }


    // TO MOVE
    void Update()
    {
        //animator.SetBool("turningLeft", false);

        if (transform.position.y < 4.0f)
        {
            transform.position = new Vector3(0, 5.13f, 0);
        }

        // if the character is in the ground, you can jump
        if (controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKey(KeyCode.UpArrow)) { Jump(); }
        }
        else { direction.y += Gravity * Time.deltaTime; }

        if (Input.GetKeyUp(KeyCode.DownArrow) && !isSliding) { StartCoroutine(Slide()); }

        

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            // Move to the right path
            desiredPath++;
            if (desiredPath >= 3) desiredPath = 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            // Move to the left path
            desiredPath--;
            if (desiredPath <= -1) desiredPath = 0;

            animator.SetBool("turningLeft", true);
        }

        //if (desiredPath == 0) { targetPosition += Vector3.left * pathDistance; }
        //else if (desiredPath == 2) { targetPosition += Vector3.right * pathDistance; }

        if (transform.position.x == -5f)
        {
            animator.SetBool("turningLeft", false);
            targetPosition.x = -5f;
        }

        transform.position = targetPosition;
    }

    private void FixedUpdate() { controller.Move(direction * Time.fixedDeltaTime); }

    // TO JUMP
    private void Jump() { direction.y = jumpForce; }

    // TO SLIDE
    private IEnumerator Slide()
    {
        // Slide turn on
        isSliding = true;

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
        isSliding = false;
        elapsedTime = 0;
        while(elapsedTime < slideDuration)
        {
            float slideAmount = Mathf.Lerp(0, 0.025f, elapsedTime/ slideDuration);
            transform.position = new Vector3(transform.position.x, startPos.y + slideAmount, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
