using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInStart : MonoBehaviour
{
    private Vector3 newPos;
    public static CameraInStart Instance;
    [SerializeField] public bool animIsStart;
    public bool HowToPlaySectionIn;
    public bool MainSectionIn;
    public bool RatShopIn;
    public bool AchievementIn;
    public GameObject player;
    public float timeBetweenFades;
    public List<CameraPosition> cameraPositions;

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

        if (MainSectionIn) StartCoroutine(SectIn(cameraPositions[0].name));
        if (HowToPlaySectionIn) StartCoroutine(SectIn(cameraPositions[1].name));
        if (RatShopIn) StartCoroutine(SectIn(cameraPositions[2].name));
        if (AchievementIn) StartCoroutine(SectIn(cameraPositions[3].name));
    }

    IEnumerator SectIn(string name)
    {
        foreach (CameraPosition position in cameraPositions)
        {
            if(position.name == name)
            {
                transform.position = position.cameraPos;
                yield return new WaitForSeconds(timeBetweenFades);
                ModifyBools("mrha");
            }
        }
    }

    public void ModifyBools(string boolName)
    {
        MainSectionIn = (boolName == "MainSectionIn");
        RatShopIn = (boolName == "RatShopIn");
        HowToPlaySectionIn = (boolName == "HowToPlayIn");
        AchievementIn = (boolName == "AchievementIn");
    }
}
