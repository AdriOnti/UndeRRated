using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public static bool ControlSectionIn;
    public static bool ControlSectionOut;
    public GameObject player;

    private void Update()
    {
        if (animIsStart)
        {
            //Time.timeScale = 0.25f;
            newPos = new Vector3(player.transform.position.x - 3f, player.transform.position.y + 4f, player.transform.position.z - 8f);
            transform.position = newPos;
        }

        if(ControlSectionIn) { StartCoroutine(CntrSectIn()); }
        if(ControlSectionOut) { StartCoroutine(CntrSectOut()); }
    }

    IEnumerator CntrSectIn()
    {
        //FadeController.instance.FadeOut();
        //yield return new WaitForSeconds(2f);
        transform.position = new Vector3(-52.37f, 4.62f, 37.18f);
        yield return new WaitForSeconds(0.5f);
        FadeController.instance.FadeIn();
    }

    IEnumerator CntrSectOut()
    {
        FadeController.instance.FadeOut();
        yield return new WaitForSeconds(2f);
        transform.position = new Vector3(-52.37f, 4.62f, 37.18f);
        yield return new WaitForSeconds(2f);
        FadeController.instance.FadeIn();
    }
}
