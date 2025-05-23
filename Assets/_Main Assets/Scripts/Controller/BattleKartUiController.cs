using UnityEngine;

public class BattleKartUiController : MonoBehaviour
{
    [Header("<b>Components")]
    [SerializeField] private GameObject rocketUi;

    private void OnEnable()
    {
        ActionHandler.RocketCollected += OnRocketCollected;
        ActionHandler.RocketLaunched += OnRocketLaunched;
    }

    private void OnDisable()
    {
        ActionHandler.RocketCollected -= OnRocketCollected;
        ActionHandler.RocketLaunched -= OnRocketLaunched;
    }

    private void OnRocketCollected()
    {
        rocketUi.SetActive(true);
    }

    private void OnRocketLaunched()
    {
        rocketUi.SetActive(false);
    }
}
