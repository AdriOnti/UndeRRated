using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadUndeRRated()
    {
        CameraInStart.animIsStart = false;
        SceneManager.LoadScene("UndeRRated");
    }
}
