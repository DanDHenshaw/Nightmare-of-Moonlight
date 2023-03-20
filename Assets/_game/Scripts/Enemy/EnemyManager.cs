using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING, COMPLETE }
    private SpawnState _state = SpawnState.COUNTING;

    [System.Serializable]
    public class Wave
    {
        [Tooltip("Name of the wave")]
        public string name;

        [Header("Enemy Types")]
        [Tooltip("Array of random enemies that could be spawned")]
        public Transform[] enemies;

        [Header("Spawning")]
        [Tooltip("Amount of enemies to spawn in this wave")]
        public float enemiesToSpawn;
        [Tooltip("Rate to spawn the enemies at")]
        public float enemySpawnRate;

        [Header("Spawn Point")]
        [Tooltip("Center position of a sphere use for random enemy spawn placement")]
        public Vector3 spawnPosition;
        [Tooltip("Radius of the sphere enemies spawn in")]
        public float spawnRadius;

        [Tooltip("Used to display gizmo in editor")]
        public Color gizmoColor = Color.green;
    }

    [Header("Wave Settings")]
    [Tooltip("All enemy waves")]
    [SerializeField] private Wave[] _waves;
    private int nextWave = 0;

    [Tooltip("Time to wait between waves")]
    [SerializeField] private float _timeBetweenWaves = 5f;
    private float _waveCountdown;

    private float _searchCountdown = 1f;

    public bool isComplete = false;

    void Start()
    {
        _waveCountdown = _timeBetweenWaves;
    }

    void Update()
    {
        if (_state == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                // Begin new round
                WaveComplete();
            }
            else
            {
                return;
            }
        }

        if (_state == SpawnState.COMPLETE)
        {
            isComplete = true;
            // Stops wave progressing
            return;
        }

        if (_waveCountdown <= 0)
        {
            if (_state != SpawnState.SPAWNING)
            {
                // Spawn Wave
                StartCoroutine(SpawnWave(_waves[nextWave]));
            } 
        }
        else
        {
            _waveCountdown -= Time.deltaTime;
        }
    }

    void WaveComplete()
    {
        if (nextWave + 1 > _waves.Length - 1)
        {
            Debug.Log("All Waves Complete!");

            _state = SpawnState.COMPLETE;
        }
        else
        {
            Debug.Log("Wave Complete!");

            _state = SpawnState.COUNTING;
            _waveCountdown = _timeBetweenWaves;

            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        _searchCountdown -= Time.deltaTime;
        if (_searchCountdown <= 0)
        {
            _searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning Wave: " + wave.name);
        _state = SpawnState.SPAWNING;

        // Spawn
        for (int i = 0; i < wave.enemiesToSpawn; i++)
        {
            SpawnEnemy(wave.enemies[Random.Range(0, wave.enemies.Length)], wave);
            yield return new WaitForSeconds(1f / wave.enemySpawnRate);
        }

        _state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemy, Wave wave)
    {
        // Spawn enemy
        Debug.Log("Spawning Enemy: " + enemy.name);

        Vector3 pos = wave.spawnPosition + new Vector3(Random.Range(-wave.spawnRadius / 2, wave.spawnRadius / 2),
            0, Random.Range(-wave.spawnRadius / 2, wave.spawnRadius / 2));
        Instantiate(enemy, pos, transform.rotation);
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            Gizmos.color = _waves[i].gizmoColor;
            Gizmos.DrawWireSphere(_waves[i].spawnPosition, _waves[i].spawnRadius);
        }
    }
}
