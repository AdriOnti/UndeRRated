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
    public int desiredPath = 1;
    private Animator animatorRat;
    public GameObject dizzyRat;





    private bool isShooting;
    private int breakableCount = 0;
    private bool isDizzy = false;
    private float slideDuration = 0.5f;
    private float jumpDuration = 0.5f;
    //private RatInputs ratInputs;


    [Header("Rat Parameters")]
    public float jumpForce;
    public float pathDistance = 9;
    public float Gravity;
    public GameObject ratParticles;

    [Header("RatBullet Parameters")]

    private Transform shootTarget;
    public float shootForce;


    private BoxCollider ratCol;
    private float defaultSizeCollider;
    private float slideableYsize = 0.1f;
    private Transform[] shootTargets;
    private GameObject[] canvas; // DeadMenu, HUD, PauseMenu


    KeyCode kright = KeyCode.RightArrow;
    KeyCode kleft = KeyCode.LeftArrow;
    KeyCode kup = KeyCode.UpArrow;
    KeyCode kdown = KeyCode.DownArrow;

    KeyCode kd = KeyCode.D;
    KeyCode ka = KeyCode.A;
    KeyCode kw = KeyCode.W;
    KeyCode ks = KeyCode.S;

    SkinnedMeshRenderer ratRenderer;
    Color initialColor;
    public static RatController Instance;


    private void Awake()
    {
        ratRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        initialColor = ratRenderer.material.color;
        Instance = this;
    }


    // METHOD START
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animatorRat = GetComponentInChildren<Animator>();



        ratCol = GetComponent<BoxCollider>();
        defaultSizeCollider = ratCol.size.y;

        canvas = GameManager.Instance.GetUI();
        shootTargets = GameManager.Instance.RatShotTarget();
    }

    // METHOD UPDATE
    void Update()
    {
        if (isDizzy)
        {
            kright = KeyCode.LeftArrow;
            kleft = KeyCode.RightArrow;
            kup = KeyCode.DownArrow;
            kdown = KeyCode.UpArrow;

            kd = KeyCode.A;
            ka = KeyCode.D;
            kw = KeyCode.S;
            ks = KeyCode.W;

            //Debug.Log("IsDizzy");
        }
        else
        {
            kright = KeyCode.RightArrow;
            kleft = KeyCode.LeftArrow;
            kup = KeyCode.UpArrow;
            kdown = KeyCode.DownArrow;

            kd = KeyCode.D;
            ka = KeyCode.A;
            kw = KeyCode.W;
            ks = KeyCode.S;
        }

        controller.Move(direction * Time.deltaTime);

        // JUMP
        if (controller.isGrounded && (Input.GetKeyDown(kup) || Input.GetKeyDown(kw))) Jump();
        else direction.y += Gravity * 2 * Time.deltaTime;

        // FORCE TO GO TO THE GROUND IF IS JUMPING
        if (!controller.isGrounded && (Input.GetKeyDown(kdown) || Input.GetKeyDown(ks))) direction.y -= jumpForce;

        // CALCULATE THE RIGHT PATH
        if (Input.GetKeyDown(kright) || Input.GetKeyDown(kd))
        {
            desiredPath++;
            if (desiredPath >= 3) desiredPath = 2;
        }

        // CALCULATE THE LEFT PATH
        if (Input.GetKeyDown(kleft) || Input.GetKeyDown(ka))
        {
            desiredPath--;
            if (desiredPath <= -1) desiredPath = 0;
        }

        // MOVE TO THE PATH
        GoToPath();

        // SLIDE
        if (controller.isGrounded && (Input.GetKeyUp(kdown) || Input.GetKeyUp(ks)))
        {
            animatorRat.SetBool("isSliding", true);
            ratCol.size = new Vector3(ratCol.size.x, slideableYsize, ratCol.size.z);
            StartCoroutine(StopSlideAnimation());
        }
        if (Input.GetKeyUp(KeyCode.Escape) && GameManager.Instance.DeadMenuActive())
        {
            GameManager.Instance.PauseGame();
        }
    }

    public IEnumerator Invincibility(float invTime)
    {
        //Debug.Log("Soy invencible");
        Physics.IgnoreLayerCollision(6, 7, true);

        Color tempColor = initialColor;
        tempColor.g = 0f;
        tempColor.b = 0f;
        ratRenderer.material.color = tempColor;


        yield return new WaitForSeconds(invTime);

        Physics.IgnoreLayerCollision(6, 7, false);

        ratRenderer.material.color = initialColor;

        if (RainbowRun.Instance.isInvincible)
        {
            RainbowRun.Instance.EndInvincibleTime();
        }

    }

    public void CallInvincibility(float invisTime)
    {

        StartCoroutine(Invincibility(invisTime));


    }

    // STOP TIME IF PLAYER IMPACT WITH AN OBSTACLE
    private void OnTriggerEnter(Collider other)
    {
        if (!ProtectionField.Instance.isActive && !RainbowRun.Instance.isInvincible)   
        {
            if (other.gameObject.CompareTag("ObstacleGeneric") || other.gameObject.CompareTag("Bat"))
            {
                Die();
            }

            if (other.gameObject.CompareTag("ObstacleBreakable"))
            {
                ratParticles.SetActive(true);
                ratParticles.GetComponent<ParticleSystem>().Play();
                Debug.Log(breakableCount);
                MeshRenderer meshBreakable = other.GetComponent<MeshRenderer>();
                meshBreakable.enabled = false;
                if (breakableCount == 0)
                {
                    isDizzy = true;
                    dizzyRat.gameObject.SetActive(true);
                    StartCoroutine(WaitAfterBreakable(0.5f, meshBreakable));
                    StartCoroutine(TimeDizzy(5f));
                }
                else if (breakableCount == 1)
                {
                    Die();
                    breakableCount = 0;
                }
            }
        }
        else if (other.CompareTag("ObstacleGeneric") || other.gameObject.CompareTag("Bat") || other.gameObject.CompareTag("ObstacleBreakable"))
        {

            StartCoroutine(Invincibility(ProtectionField.Instance.invincibleTime));
            ProtectionField.Instance.Protect();
        }


        if (other.gameObject.CompareTag("Cheese"))
        {
            Score.AddCheese(1);
            other.transform.SetParent(ObjectsPool.instance.transform);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("MegaCheese"))
        {
            Score.AddCheese(5);
            other.transform.SetParent(ObjectsPool.instance.transform);
            other.gameObject.SetActive(false);
        }
        //Time.timeScale = 0;
    }




    // JUMP FUNCTION
    private void Jump()
    {

        animatorRat.SetBool("isJumping", true);
        animatorRat.SetBool("isSliding", false);
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
        animatorRat.SetBool("isJumping", false);
    }
    private IEnumerator StopSlideAnimation()
    {

        yield return new WaitForSeconds(slideDuration);
        animatorRat.SetBool("isSliding", false);
        ratCol.size = new Vector3(ratCol.size.x, defaultSizeCollider, ratCol.size.z);
    }

    public IEnumerator EndShootingAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        animatorRat.SetBool("isShooting", false);
    }

    private void Die()
    {
        // Time.timeScale = 0;
        ratParticles.SetActive(true);
        ratParticles.GetComponent<ParticleSystem>().Play();

        animatorRat.SetBool("isDead", true);
        RoadTileMove.speed = 0;
        //foreach (GameObject menu in canvas)
        //{
        //    if (menu.name != "DeadMenu") menu.SetActive(false);
        //    else menu.SetActive(true);
        //}
        GameManager.Instance.DeadCharacter();
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
        dizzyRat.gameObject.SetActive(false);
    }
}

