using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Types")]
    [SerializeField] private GameObject[] _enemies;

    [Header("Spawning")] 
    [SerializeField] private float _enemiesToSpawn;
}
