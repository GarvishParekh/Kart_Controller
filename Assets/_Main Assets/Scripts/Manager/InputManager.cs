using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("<b>Scriptable")]
    [SerializeField] private InputData inputData;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        ReadKeyboardInputs();
        CheckForRestart();
    }

    private void ReadKeyboardInputs()
    {
        inputData.zKeybaord = Input.GetAxis("Vertical");
        inputData.xKeybaord = Input.GetAxis("Horizontal");
    }

    private void CheckForRestart()
    {
        if (Input.GetKeyDown(KeyCode.R)) ActionHandler.RestartScene?.Invoke();
    }
}
