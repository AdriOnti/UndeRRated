using System.Collections;
using UnityEngine;

public class CameraInStart : MonoBehaviour
{
    private Vector3 newPos;
    public static CameraInStart Instance;
    public bool animIsStart;
    public bool ControlSectionIn;
    public bool MainSectionIn;
    public bool ShopSectionIn;
    public GameObject player;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    private void Update()
    {
        if (animIsStart)
        {
            newPos = new Vector3(player.transform.position.x - 3f, player.transform.position.y + 4f, player.transform.position.z - 8f);
            transform.position = newPos;
        }

        if(ControlSectionIn) { StartCoroutine(CntrSectIn()); }
        if(MainSectionIn) { StartCoroutine(MainSectIn()); }
        if(ShopSectionIn) StartCoroutine(ShopSectIn());
    }

    IEnumerator CntrSectIn()
    {
        transform.position = new Vector3(-52.37f, 4.62f, 36.18f);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator MainSectIn()
    {
        transform.position = new Vector3(2.22795f, 8f, 32.13271f);
        yield return new WaitForSeconds(0.5f);
        MainSectionIn = false;
    }

    IEnumerator ShopSectIn()
    {
        transform.position = new Vector3(3.23f, 104.54f, 36.61f);
        yield return new WaitForSeconds(0.5f);
    }
}
