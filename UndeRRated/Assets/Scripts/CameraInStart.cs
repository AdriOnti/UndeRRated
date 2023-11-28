using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInStart : MonoBehaviour
{
    //public static bool animIsStart;

    //void FixedUpdate()
    //{
    //    if (animIsStart)
    //    {
    //        transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y - 0.2f, transform.position.z + 0.5f);
    //    }
    //}

    private Vector3 newPos;
    public static bool animIsStart;
    public GameObject player;

    private void Update()
    {
        if (animIsStart)
        {
            Time.timeScale = 0.25f;
            newPos = new Vector3(player.transform.position.x - 5f, player.transform.position.y + 7f, player.transform.position.z - 13f);
            transform.position = newPos;
        }
    }
}
