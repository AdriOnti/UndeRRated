using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratatatata : Ability
{

    private Transform shootTarget;
    public GameObject bullet;
    private bool isShooting;
    private Animator animatorRat;
    public float shootForce;
    private Transform[] shootTargets;


    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
       
    }

    private void Update()
    {
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
    }
    private void Start()
    {
        animatorRat = GetComponentInChildren<Animator>();
        shootTargets = GameManager.Instance.RatShotTarget();
    }
    // Update is called once per frame
    public override void Cast()
    {
        shootTarget = ShotTarget();
        try
        {
            bullet = ObjectsPool.instance.GetPooledRatBullet();
        }
        catch
        {
            Debug.Log("No hay m�s balas disponibles");
            bullet = null;
        }

        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);

            //bullet.GetComponent<RatBullet>().StartMovement(shootTarget.position, shootForce);
            isShooting = true;
            animatorRat.SetBool("isShooting", true);
            StartCoroutine(EndShootingAnimation());
        }
    }
    public IEnumerator EndShootingAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        animatorRat.SetBool("isShooting", false);
    }
    private Transform ShotTarget()
    {
        if (RatController.Instance.desiredPath == 0) return shootTargets[0];
        if (RatController.Instance.desiredPath == 1) return shootTargets[1];
        if (RatController.Instance.desiredPath == 2) return shootTargets[2];
        return null;
    }
}