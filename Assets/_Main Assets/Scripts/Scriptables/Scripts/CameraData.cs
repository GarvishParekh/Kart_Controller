using UnityEngine;

[CreateAssetMenu(fileName = "Camera data", menuName = "Scriptable/Camera data")]
public class CameraData : ScriptableObject
{
    public float cameraFollowSmoothness;
    public float cameraRotationSmoothness;
}
