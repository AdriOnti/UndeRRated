
using UnityEditor;
using UnityEngine;

public class AttackingBat : ObstacleRespawner
{
    // PARAMETERS
    public GameObject projectile;
    public Transform parentRoad;
    private bool isShooting;
    [SerializeField] Transform[] shootTargets = new Transform[3];
    private int targetIndex;
    private float speed = 150f;

    // START FUNCTION
    /// <summary>
    /// La posicion del proyectil es la misma que la del murci�lago
    /// </summary>
    void Start()
    {
        projectile.transform.position = transform.position;
        shootTargets = GameManager.Instance.BatTarget();
    }

    // AWAKE FUNCTION
    /// <summary>
    /// Selecciona uno de los tres targets
    /// </summary>
    void Awake()
    {
        System.Random rnd = new System.Random();
        targetIndex = rnd.Next(shootTargets.Length);
    }

    // UPDATE FUNCTION
    /// <summary>
    /// Si el enemigo esta disparando, el proyectil se movera hacia el objetivo. Cuando llegue al a una distancia minima al objetivo, 
    /// el murci�lago dejara de disparar y el proyectil se convertira en hijo de la carretera.
    /// </summary>
    private void Update()
    {
        if (isShooting)
        {
            // projectile.transform.SetParent(parent);
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, shootTargets[targetIndex].position, speed * Time.deltaTime);
            if (Vector3.Distance(projectile.transform.position, shootTargets[targetIndex].position) < 0.01f)
            {
                isShooting = false;
                projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y - 1.5f, projectile.transform.position.z);
                projectile.transform.SetParent(parentRoad);
            }
        }
    }
   
    /// <summary>
    /// Cuando el murci�lago llegue al trigger collider con tag AttackTrigger, ejecutara la funcion Shoot()
    /// </summary>
    /// <param name="other">Trigger que tendra que comparar para saber si ejecutar una funcion o no</param>
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("AttackTrigger"))
        {
            Shoot();
        }
        else if (/*other.CompareTag("Ground") ||*/ other.CompareTag("Player") || other.CompareTag("RatBullet"))
        {
            this.gameObject.SetActive(false);
            projectile.SetActive(false);
        }

    }

    // SHOOT FUNCTION
    /// <summary>
    /// Lo primero que hara sera obtener una PoisonBall de la pool de obstaculos. Luego comprobamos si hay algun proyectil, si hay uno, lo activamos y hacemos que el enemigo pueda disparar al activar su bool
    /// </summary>
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

    /// <summary>
    /// Mostramos su animacion de muerte, se espera unos segundos y luego se desactiva
    /// </summary>
    /// <param name="collision">Objeto con el que colisiona para morir</param>
    private void OnCollisionEnter(Collision collision)
    {
        // Play Dead Animation
        // Call a IEnumerator with a WaitForSeconds
        this.gameObject.SetActive(false);
    }
}
