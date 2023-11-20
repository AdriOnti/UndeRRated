using System;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI score;
    static float scoreAmount;
    public float pointIncreastedPerSec;
    static int killPoints = 10;
    protected bool enemyKilled;


    // Start is called before the first frame update
    void Start()
    {
        scoreAmount = 0f;
        pointIncreastedPerSec = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score.text = Convert.ToInt32(scoreAmount).ToString();
        scoreAmount += pointIncreastedPerSec * Time.deltaTime*2;
    }
    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    scoreAmount += ExtraPoints();
        //    enemyKilled = false;    
        //}
    }
    public static void ExtraPoints()
    {
        scoreAmount += killPoints;
    }
}
