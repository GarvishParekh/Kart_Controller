using UnityEngine;

[CreateAssetMenu(fileName = "Kart data", menuName = "Scriptable/Kart data")]
public class KartData : ScriptableObject
{
    public RocketCollected rocketCollected;
    public float kartSpeed = 5;
    public float kartTurningSpeed = 20;
    public float kartDownForce = -2;

    public float kartMaxTurn = 30;
    public float kartMinTurn = 5;

}
