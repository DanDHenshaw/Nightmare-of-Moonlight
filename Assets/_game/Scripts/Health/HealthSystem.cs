using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HealthData))]
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private HealthData _data;

    private float _health;

    void Awake()
    {
        _health = _data.maxHealth;
    }

    void Update()
    {
        if (_health <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    private void Death()
    {
        switch (_data.type)
        {
            case HealthData.Type.Player:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
                break;
            case HealthData.Type.Enemy:
                break;
            case HealthData.Type.Boss:
                break;
        }
    }
}
