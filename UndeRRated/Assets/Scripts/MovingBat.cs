using UnityEngine;
using System.Collections;
using UnityEditor;

public class MovingBat : ObstacleRespawner
{
    public Transform[] attackingPositions = new Transform[3];
    public Transform parentRoad;
    private bool isMoving;
    private float speed = 150f;
    private int targetIndex;
   
    Light lightWarning;
    float flashDuration = 0.5f;
    int flashNumber = 4;


    // Start is called before the first frame update
    void Awake()
    {
         System.Random rnd = new System.Random();
         targetIndex = rnd.Next(attackingPositions.Length); 
    }
    private void Update()
    {
        if (isMoving)
        {
            transform.SetParent(parent);
            transform.position = Vector3.MoveTowards(transform.position, attackingPositions[targetIndex].position, speed * Time.deltaTime);
           
            if (Vector3.Distance(transform.position, attackingPositions[targetIndex].position) < 0.01f)
            {

                isMoving = false;
                transform.SetParent(parentRoad);

            }
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("AttackTrigger"))
        {
            isMoving = true;
        }
        else if (other.CompareTag("WarningTrigger"))
        {

            lightWarning = attackingPositions[targetIndex].GetComponent<Light>();
            StartCoroutine(flashNow());
            lightWarning.enabled = false;
        }
        else if (/*other.CompareTag("Ground") ||*/ other.CompareTag("Player")/*other.CompareTag("RatBullet")*/)  
        {
           this.gameObject.SetActive(false);
            
        }
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Animation of death
        
    //}
    public IEnumerator flashNow()
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
