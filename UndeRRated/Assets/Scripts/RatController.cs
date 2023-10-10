using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    // PARAMETERS
    private CharacterController controller;
    private Vector3 direction;

    private int desiredPath = 1;    // 0: Left Path, 1: Middle Path, 2: Right Path
    public float pathDistance = 4;

    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;
    private bool isSliding;

    // METHODS
    void Start() { controller = GetComponent<CharacterController>(); }


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

        // Move to the right path
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            desiredPath++;
            if (desiredPath >= 3) desiredPath = 2;
        }

        // Move to the left path
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            desiredPath--;
            if (desiredPath <= -1) desiredPath = 0;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredPath == 0) { targetPosition += Vector3.left * pathDistance; }
        else if (desiredPath == 2) { targetPosition = Vector3.right * pathDistance; }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);
    }

    private void FixedUpdate() { controller.Move(direction * Time.fixedDeltaTime); }

    // TO JUMP
    private void Jump() { direction.y = jumpForce; }

    // TO SLIDE
    private IEnumerator Slide()
    {
        // Slide turn on
        isSliding = true;
        //animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);

        // Slide turn off
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        //animator.SetBool("isSliding", false);
        isSliding = false;
    }
}
