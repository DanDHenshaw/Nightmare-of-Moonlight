using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    private EnemyManager _enemyManager;

    [Tooltip("Next level to load for the player")]
    [SerializeField] private string _nextLevelName;

    private bool _isComplete;

    void Awake()
    {
        _enemyManager = GameObject.FindObjectOfType<EnemyManager>();
    }

    void OnTriggerEnter(Collider colldier)
    {
        if (!colldier.CompareTag("Player")) { return; }

        if (_enemyManager.isComplete)
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        Debug.Log("Load next scene");
        SceneManager.LoadSceneAsync(_nextLevelName, LoadSceneMode.Single);
    }
}
