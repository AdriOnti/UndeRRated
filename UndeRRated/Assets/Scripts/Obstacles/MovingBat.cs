using UnityEngine;
using System.Collections;

public class MovingBat : ObstacleRespawner
{
    // PARAMETERS
    private Transform[] attackingPositions = new Transform[3];
    public Transform parentRoad;
    private bool isMoving;
    private float speed = 150f;
    private int targetIndex;
    Light lightWarning;
    float flashDuration = 0.5f;
    int flashNumber = 4;

    private void Start()
    {
        attackingPositions = GameManager.Instance.BatTarget(); 
        System.Random rnd = new System.Random();
        targetIndex = rnd.Next(attackingPositions.Length);
    }
    // UPDATE FUNCTION
    /// <summary>
    /// Si el murciélago tiene su booleano de moverse en true, cada frame se ira moviendo poco a poco hasta llegar a una distancia minima
    /// </summary>
    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackingPositions[targetIndex].position, speed * Time.deltaTime);   
            if (Vector3.Distance(transform.position, attackingPositions[targetIndex].position) < 0.01f) isMoving = false;
        }
    }

    /// <summary>
    /// Cuando el murciélago llegue al trigger collider con tag AttackTrigger, se pondra en movimiento.
    /// Si interactua con el WarningTrigger la AttackingPositions seleccionada se convertira en la que avisara del ataque.
    /// </summary>
    /// <param name="other">Trigger que tendra que comparar para saber si ejecutar una funcion o no</param>
    public override void OnTriggerEnter(Collider other)
    {
     
        base.OnTriggerEnter(other);
        if (other.CompareTag("AttackTrigger"))
        {
            isMoving = true;
          
        }
        else if (other.CompareTag("WarningTrigger"))
        {
            lightWarning = attackingPositions[targetIndex].GetComponent<Light>();
            StartCoroutine(FlashNow());
            lightWarning.enabled = false;
            SoundManager.Instance.PlaySound(Audios.BatIdle_2);
        }
        else if (/*other.CompareTag("Ground") ||*/ other.CompareTag("Player") || other.CompareTag("RatBullet"))  
        {
            // Play Death Anim
            // WaitForSeconds            
            lightWarning.enabled = false;
            isMoving = false;
            //StartCoroutine(Die());
        }
       

    }

    // EXECUTE FLASH
    /// <summary>
    /// Cuando se ejecute esta Corrutina encendera y apagara la luz durante lo que indique la variable flashNumber
    /// </summary>
    /// <returns>Devuelve un WaitForSeconds</returns>
    public IEnumerator FlashNow()
    {
        for(int i = 0; i < flashNumber; i++)
        {
            lightWarning.enabled = true;
            yield return new WaitForSeconds(flashDuration / 2);
            lightWarning.enabled = false;
            yield return new WaitForSeconds(flashDuration / 2);
        }
    }
}
