using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void OnEnable()
    {
        ActionHandler.RestartScene += OnRestartScene;
    }

    private void OnDisable()
    {
        ActionHandler.RestartScene -= OnRestartScene;
    }

    private void OnRestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
