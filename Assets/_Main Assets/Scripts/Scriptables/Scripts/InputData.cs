using UnityEngine;

[CreateAssetMenu (fileName = "Input data", menuName = "Scriptable/Input data")]
public class InputData : ScriptableObject
{
    [Header("<b>Controls")]
    public ControlType controlType;

    [Header("<b>Keyboard")]
    public float zKeybaord;
    public float xKeybaord;

    [Header("<b>Gyro")]
    public float xGyro;
    public float zButton;
}
