using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("<b>Scriptable")]
    [SerializeField] private CameraData cameraData;

    [Header("<b>Components")]
    [SerializeField] private Transform followTarget;

    private void FixedUpdate()
    {
        FollowController();
    }

    private void FollowController()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position, cameraData.cameraFollowSmoothness * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, followTarget.rotation, cameraData.cameraRotationSmoothness * Time.deltaTime);
    }
}
