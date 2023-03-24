using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Player/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float attackCooldown;

    [Header("Right")]
    [SerializeField] private Vector3 rightPos;
    [SerializeField] private Vector3 rightRot;

    [Header("Left")]
    [SerializeField] private Vector3 leftPos;
    [SerializeField] private Vector3 leftRot;
}
