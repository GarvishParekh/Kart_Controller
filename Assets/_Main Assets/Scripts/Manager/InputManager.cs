using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("<b>Scriptable")]
    [SerializeField] private InputData inputData;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        switch (inputData.controlType)
        {
            case ControlType.GYRO: Input.gyro.enabled = true; break;
        }
    }

    private void Update()
    {
        switch (inputData.controlType)
        {
            case ControlType.KEYBOARD:
                ReadKeyboardInputs();
                break;
            case ControlType.GYRO:
                ReadGyro();
                break;
        }
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

    private void ReadGyro()
    {
        // Example: Tilt left/right maps to -1 to 1
        float tilt = Input.gyro.attitude.eulerAngles.z;

        if (tilt > 180f) tilt -= 360f; // Normalize Z tilt from -180 to 180
        tilt = Mathf.Clamp(tilt, -30f, 30f); // Limit extreme values
        inputData.xGyro = tilt / 30f; // Map to -1 to 1
    }

    public void B_AccelerationDown() => inputData.zButton = 1;
    public void B_AccelerationUp() => inputData.zButton = 0;
    public void B_BreaksDown() => inputData.zButton = -1;
    public void B_BreaksUp() => inputData.zButton = 0;
}
