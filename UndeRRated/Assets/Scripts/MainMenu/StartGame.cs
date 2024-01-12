using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadUndeRRated()
    {
        StartCoroutine(Comienza());
    }

    IEnumerator Comienza()
    {
        CameraInStart.Instance.animIsStart = false;
        FadeController.instance.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("UndeRRated");
    }
}
