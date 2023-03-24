using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Player/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Stats")]
    public float damage;
    public float range;
    public float attackCooldown;
}
