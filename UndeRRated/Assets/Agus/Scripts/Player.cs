using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    private Vector3 direction;


    private int desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 7; //Distane between two lanes
    // Start is called before the first frame update

    public float jumpForce;
    public float Gravity = -20;

    void Start()
    {
        controller = GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move(direction * Time.deltaTime);

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        } else
        {
            direction.y += Gravity * Time.deltaTime;
        }
       
        // Direction arrows
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3) desiredLane = 2;

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1) desiredLane = 0;
        }

        //Calculate where will move the player

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        } else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
 


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObstacleGeneric")) Time.timeScale = 0;
    }
    private void Jump()
    {
        direction.y = jumpForce;
    }


}
