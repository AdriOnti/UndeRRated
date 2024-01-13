using UnityEngine;

[CreateAssetMenu(fileName = "New Camera Position", menuName = "Camera Position")]
public class CameraPosition : ScriptableObject
{
    public string sectionName;
    public Vector3 cameraPos;
}