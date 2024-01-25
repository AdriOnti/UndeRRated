using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public ClickeableObj clickableRat;
    public void LoadUndeRRated()
    {
        StartCoroutine(Comienza());
        clickableRat.enabled = false;
    }

    IEnumerator Comienza()
    {
        CameraInStart.Instance.animIsStart = false;
        FadeController.instance.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("UndeRRated");
    }
}
