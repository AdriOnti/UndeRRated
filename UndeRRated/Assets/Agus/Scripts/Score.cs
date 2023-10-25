using System;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI score;
    public float scoreAmount;
    public float pointIncreastedPerSec;
    private int killPoints = 10;
    private bool enemyKilled= true;


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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            scoreAmount += ExtraPoints();
            enemyKilled = false;    
        }
    }
    private int ExtraPoints()
    {
        return killPoints;
    }
}
