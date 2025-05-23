using UnityEngine;
using System.Collections;

public class CollectableController : MonoBehaviour
{
    [Header("<b>Components")]
    [SerializeField] private GameObject boxMesh;

    bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.CompareTag ("Player"))
        {
            ActionHandler.RocketCollected?.Invoke();
            boxMesh.SetActive (false);
            isCollected = true;
            StartCoroutine(nameof(SetCollectableActive));
        }
    }

    private IEnumerator SetCollectableActive()
    {
        yield return new WaitForSeconds(5);
        boxMesh.SetActive(true);
        isCollected = false;
    }
}
