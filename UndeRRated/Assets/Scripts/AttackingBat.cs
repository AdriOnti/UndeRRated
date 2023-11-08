
using UnityEditor;
using UnityEngine;

public class AttackingBat : MonoBehaviour
{

    public GameObject projectile;
    public Transform parent;
    public Transform parentRoad;
    private bool isShooting;
    public Transform[] shootTargets = new Transform[3];
    private int targetIndex;
    private float speed = 150f;




    void Start()
    {
        projectile.transform.position = transform.position;
    }

    void Awake()
    {
        System.Random rnd = new System.Random();
        targetIndex = rnd.Next(shootTargets.Length);
    }

    private void Update()
    {
        if (isShooting)
        {
           // projectile.transform.SetParent(parent);
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, shootTargets[targetIndex].position, speed * Time.deltaTime);
            if (Vector3.Distance(projectile.transform.position, shootTargets[targetIndex].position) < 0.01f)
            {
                isShooting = false;
                projectile.transform.SetParent(parentRoad);
            }
        }
    }
    private void OnBecameInvisible()
    {
        try
        {
            this.gameObject.transform.SetParent(parent);
            this.gameObject.SetActive(false);
        }
        catch { }


    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("AttackTrigger"))
        {
            Shoot();
        }

    }
    void Shoot()
    {
        projectile = ObjectsPool.instance.GetPooledPoisonBall();
        projectile.transform.position = transform.position;

        if (projectile != null)
        {
            projectile.SetActive(true);
            isShooting = true;
        }

    }


    private void OnCollisionEnter(Collision collision)
    {

        //Llamar a la animación de muerte
        //WaitForSeconds
        this.gameObject.SetActive(false);
    }
}
