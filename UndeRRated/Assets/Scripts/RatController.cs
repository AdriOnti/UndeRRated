using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RatController : MonoBehaviour
{
    // PRIVATE PARAMETERS
    private CharacterController controller;
    private Vector3 direction;
    private int desiredPath = 1;
    private Animator animator;
    private bool isShooting;

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

    // METHOD START
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        bullet.transform.position = transform.position;
    }

    // METHOD UPDATE
    void Update()
    {
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
            //  animator.Play("slide");
            // StartCoroutine(StopAnimation());
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
            Time.timeScale = 0;
            foreach (GameObject menu in canvas)
            {
                if (menu.name != "DeadMenu") menu.SetActive(false);
                else menu.SetActive(true);
            }
        }

    }


    // JUMP FUNCTION
    private void Jump() { direction.y = jumpForce; }

    // MOVEMENT TO THE DESIRED PATH FUNCTION
    private void GoToPath()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredPath == 0) targetPosition += Vector3.left * pathDistance;
        if (desiredPath == 2) targetPosition += Vector3.right * pathDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.25f);
    }

    // STOP ANIMATION
    private IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.enabled = false;
        yield return new WaitForSeconds(0.01f);
        animator.enabled = true;
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
        }
    }

    private Transform ShotTarget()
    {
        if (desiredPath == 0) return shootTargets[0];
        if (desiredPath == 1) return shootTargets[1];
        if (desiredPath == 2) return shootTargets[2];
        return null;
    }
}