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
    public float jumpForce;
    public float pathDistance = 10;
    public float Gravity;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        controller.Move(direction * Time.deltaTime);

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.UpArrow)) Jump();
        else direction.y += Gravity*2 * Time.deltaTime;

        if (!controller.isGrounded && Input.GetKeyDown(KeyCode.DownArrow)) direction.y -= jumpForce;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredPath++;
            if (desiredPath >= 3) desiredPath = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredPath--;
            if (desiredPath <= -1) desiredPath = 0;
        }

        GoToPath();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObstacleGeneric") || other.gameObject.CompareTag("ObstacleHN") || other.gameObject.CompareTag("ObstacleHT")) Time.timeScale = 0;
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void GoToPath()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredPath == 0) targetPosition += Vector3.left * pathDistance;
        if (desiredPath == 2) targetPosition += Vector3.right * pathDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.25f);
    }
}