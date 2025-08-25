using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpSO", menuName = "PowerUpSO")]
public class PowerUpSO : ScriptableObject
{
    [SerializeField] string powerupType;
    [SerializeField] float valueChange;
    [SerializeField] float time;

    public string GetPowerUpType()
    {
        return powerupType;
    }

    public float GetValueChange()
    {
        return valueChange;
    }

    public float GetTime()
    {
        return time;
    }
}
