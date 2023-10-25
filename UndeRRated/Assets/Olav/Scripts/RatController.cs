using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class RatController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private int desiredPath = 1;
    private Animator animator;

    public float jumpForce;
    public float pathDistance = 10;
    public float Gravity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        controller.Move(direction * Time.deltaTime);

        // JUMP
        if(controller.isGrounded && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))) Jump(); 
        else direction.y += Gravity * 2 * Time.deltaTime;

        // GO TO GROUND IN MIDDLE OF THE JUMP
        if(!controller.isGrounded && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))) direction.y -= jumpForce;

        // CALCULATE THE RIGHT PATH
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            desiredPath++;
            if (desiredPath >= 3) desiredPath = 2;
        }

        // CALCULATE THE LEFT PATH
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            desiredPath--;
            if(desiredPath <= -1) desiredPath = 0;
        }

        // MOVE TO THE PATH
        GoToPath();

        // SLIDE
        if (controller.isGrounded && (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)))
        {
            animator.Play("slide");
            StartCoroutine(StopAnimation());
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObstacleGeneric")) Time.timeScale = 0;
    }

    private void Jump() { direction.y = jumpForce; }

    private void GoToPath()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredPath == 0) targetPosition += Vector3.left * pathDistance;
        if (desiredPath == 2) targetPosition += Vector3.right * pathDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.25f);
    }

    private IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.enabled = false;
        yield return new WaitForSeconds(0.5f);
        animator.enabled = true;
    }
}
