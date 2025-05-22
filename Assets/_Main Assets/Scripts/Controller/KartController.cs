using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class KartController : MonoBehaviour
{
    Rigidbody kartRb;

    [Header("<b>Scriptable")]
    [SerializeField] private InputData inputData;
    [SerializeField] private KartData kartData;

    [Header("<b>Components")]
    [SerializeField] private Transform carVisual;
    [SerializeField] private LayerMask roadLayer;

    float kartAcceleration = 0;
    float kartRotation = 0;

    private void Awake()
    {
        kartRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Acceleration();
        ApplyKartAcceleration();
        ApplyHandleControls();
        CarOrientation();
    }

    private void Acceleration()
    {
        kartAcceleration = Mathf.MoveTowards(kartAcceleration, kartData.kartSpeed * inputData.zKeybaord, 20 * Time.deltaTime);
    }

    private void ApplyKartAcceleration()
    {
        float currentY = kartRb.velocity.y;
        kartRb.velocity = kartRb.transform.forward * kartAcceleration;
        kartRb.velocity = new Vector3(kartRb.velocity.x, currentY, kartRb.velocity.z);
    }

    private void ApplyHandleControls()
    {
        transform.Rotate(0, inputData.xKeybaord * kartData.kartTurningSpeed, 0);
    }

    private void CarOrientation()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 12))
        {
            Vector3 groundNormal = hit.normal;

            // First align car with ground normal
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
            Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, 12 * Time.fixedDeltaTime);

            // Calculate the forward direction projected on the ground
            Vector3 forward = Vector3.ProjectOnPlane(transform.forward, groundNormal).normalized;
            smoothedRotation = Quaternion.LookRotation(forward, groundNormal);

            // Convert to Euler angles to clamp the Z axis (roll)
            Vector3 euler = smoothedRotation.eulerAngles;

            // Normalize Z to range -180 to 180
            float z = euler.z > 180 ? euler.z - 360 : euler.z;

            // Clamp Z rotation
            z = Mathf.Clamp(z, -20f, 20f);

            // Reconstruct rotation with clamped Z
            euler.z = z;
            carVisual.rotation = Quaternion.Euler(euler);
        }
    }
}
