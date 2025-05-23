using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("<b>Scriptable")]
    [SerializeField] private InputData inputData;
    [SerializeField] private KartData kartData;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        switch (inputData.controlType)
        {
            case ControlType.GYRO: Input.gyro.enabled = true; break;
        }
    }

    private void OnEnable()
    {
        ActionHandler.RocketCollected += OnRocketCollected;
    }

    private void OnDisable()
    {
        ActionHandler.RocketCollected -= OnRocketCollected;
    }

    private void OnRocketCollected() => kartData.rocketCollected = RocketCollected.COLLECTED;

    private void Update()
    {
        switch (inputData.controlType)
        {
            case ControlType.KEYBOARD:
                ReadKeyboardInputs();
                ReadRocketLaunch();
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

    private void ReadRocketLaunch()
    {
        if (kartData.rocketCollected == RocketCollected.NOT_COLLECTED) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ActionHandler.RocketLaunched?.Invoke();
        }
    }

    private void CheckForRestart()
    {
        if (Input.GetKeyDown(KeyCode.R)) ActionHandler.RestartScene?.Invoke();
    }

    float tiltValue = 0;
    float sensitivity = 2;
    private void ReadGyro()
    {
        float rawTilt = Input.acceleration.x;

        // Clamp value between -1 and 1
        tiltValue = Mathf.Clamp(rawTilt * sensitivity, -1f, 1f);
        inputData.xGyro = tiltValue;
    }

    public void B_AccelerationDown() => inputData.zButton = 1;
    public void B_AccelerationUp() => inputData.zButton = 0;
    public void B_BreaksDown() => inputData.zButton = -1;
    public void B_BreaksUp() => inputData.zButton = 0;
}
