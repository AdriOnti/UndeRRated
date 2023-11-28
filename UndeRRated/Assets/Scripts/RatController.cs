using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RatController : MonoBehaviour
{
    // PRIVATE PARAMETERS
    private CharacterController controller;
    private Vector3 direction;
    private int desiredPath = 1;
    private Animator animator;
    private bool isShooting;
    private int breakableCount = 0;
    private bool isDizzy = false;
    private float slideDuration = 0.5f;
    private float jumpDuration = 0.5f;
    //private RatInputs ratInputs;


    // PUBLIC PARAMETERS
    public float jumpForce;
    public float pathDistance = 9;
    public float Gravity;
    public List<GameObject> canvas; // DeadMenu, HUD, PauseMenu

    // RAT BULLET PARAMETERS
    public GameObject bullet;
    public Transform[] shootTargets = new Transform[3];
    private Transform shootTarget;
    public float shootForce;


    private BoxCollider ratCol;
    private float defaultSizeCollider;
    private float slideableYsize = 0.1f;

    // METHOD START
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        bullet.transform.position = transform.position;
        ratCol = GetComponent<BoxCollider>();
        defaultSizeCollider = ratCol.size.y;
    }

    // METHOD UPDATE
    void Update()
    {
        if (isDizzy)
        {
            //Debug.Log("IsDizzy");
            // Input System
        }

        controller.Move(direction * Time.deltaTime); 
       
        // JUMP
        if (controller.isGrounded && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))) Jump();
        else direction.y += Gravity * 2 * Time.deltaTime;

        // FORCE TO GO TO THE GROUND IF IS JUMPING
        if (!controller.isGrounded && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))) direction.y -= jumpForce;

        // CALCULATE THE RIGHT PATH
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            desiredPath++;
            if (desiredPath >= 3) desiredPath = 2;
        }

        // CALCULATE THE LEFT PATH
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            desiredPath--;
            if (desiredPath <= -1) desiredPath = 0;
        }

        // MOVE TO THE PATH
        GoToPath();

        // SLIDE
        if (controller.isGrounded && (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)))
        {
            animator.SetBool("isSliding", true);
            ratCol.size = new Vector3(ratCol.size.x, slideableYsize, ratCol.size.z);
            StartCoroutine(StopSlideAnimation());
        }

        if (Input.GetKeyUp(KeyCode.Space)) { Shot(); }

        if (isShooting)
        {
            // projectile.transform.SetParent(parent);
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, shootTarget.position, shootForce * Time.deltaTime);
            if (Vector3.Distance(bullet.transform.position, shootTarget.position) < 0.01f)
            {
                isShooting = false;
                //bullet.transform.SetParent(parentRoad);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0;
            foreach (GameObject menu in canvas)
            {
                GetComponent<RatController>().enabled = false;
                if (menu.name != "PauseMenu") menu.SetActive(false);
                else menu.SetActive(true);
            }
        }
    }

    // STOP TIME IF PLAYER IMPACT WITH AN OBSTACLE
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("ObstacleGeneric") || other.gameObject.CompareTag("Bat"))
        {
            Die();
        }

        if (other.gameObject.CompareTag("ObstacleBreakable"))
        {
            Debug.Log(breakableCount);
            MeshRenderer meshBreakable = other.GetComponent<MeshRenderer>();
            meshBreakable.enabled = false;
            if (breakableCount == 0)
            {
                isDizzy = true;
                StartCoroutine(WaitAfterBreakable(0.5f, meshBreakable));
                StartCoroutine(TimeDizzy(5f));
            }
            else if (breakableCount == 1)
            {
                Die();
                breakableCount = 0;
            }
        }

        if (other.gameObject.CompareTag("Cheese"))
        {
            Score.AddCheese();
            other.transform.SetParent(ObjectsPool.instance.transform);
            other.gameObject.SetActive(false);
        } 
            //Time.timeScale = 0;
        }

    // JUMP FUNCTION
    private void Jump() {

        animator.SetBool("isJumping", true);
        animator.SetBool("isSliding", false);
        direction.y = jumpForce;
        StartCoroutine(StopJumpAnimation());

    }

    // MOVEMENT TO THE DESIRED PATH FUNCTION
    private void GoToPath()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredPath == 0) targetPosition += Vector3.left * pathDistance;
        if (desiredPath == 2) targetPosition += Vector3.right * pathDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.25f);
    }


    // STOP ANIMATION
    private IEnumerator StopJumpAnimation()
    {

        yield return new WaitForSeconds(jumpDuration);
        animator.SetBool("isJumping", false);
    }
    private IEnumerator StopSlideAnimation()
    {

        yield return new WaitForSeconds(slideDuration);
        animator.SetBool("isSliding", false);
        ratCol.size = new Vector3(ratCol.size.x, defaultSizeCollider, ratCol.size.z);
    }

    // SHOT
    private void Shot()
    {
        shootTarget = ShotTarget();
        try
        {
            bullet = ObjectsPool.instance.GetPooledRatBullet();
        }
        catch
        {
            Debug.Log("No hay más balas disponibles");
            bullet = null;
        }

        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);

            //bullet.GetComponent<RatBullet>().StartMovement(shootTarget.position, shootForce);
            isShooting = true;
            animator.SetBool("isShooting", true);
            StartCoroutine(EndShootingAnimation());
        }
    }
    
    public IEnumerator EndShootingAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isShooting", false);
    }

    private void Die()
    {
       // Time.timeScale = 0;

        animator.SetBool("isDead", true);
        RoadTileMove.speed = 0;
        foreach (GameObject menu in canvas)
        {
            if (menu.name != "DeadMenu") menu.SetActive(false);
            else menu.SetActive(true);
        }
    }

    private Transform ShotTarget()
    {
        if (desiredPath == 0) return shootTargets[0];
        if (desiredPath == 1) return shootTargets[1];
        if (desiredPath == 2) return shootTargets[2];
        return null;
    }

    private IEnumerator WaitAfterBreakable(float segs, MeshRenderer mesh)
    {
        yield return new WaitForSeconds(segs);
        breakableCount = 1;
        mesh.enabled = true;
    }

    private IEnumerator TimeDizzy(float segs)
    {
        yield return new WaitForSeconds(segs);
        isDizzy = false;
        breakableCount = 0;
    }
}

