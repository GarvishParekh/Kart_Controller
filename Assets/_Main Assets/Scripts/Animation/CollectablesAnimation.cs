using UnityEngine;

public class CollectablesAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private void FixedUpdate()
    {
        Animation();
    }

    private void Animation()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
