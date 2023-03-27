using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Data/Health/Health Data")]
public class HealthData : ScriptableObject
{
    public enum Type
    {
        Player,
        Enemy,
        Boss
    }
    public Type type;

    public float maxHealth;
}
