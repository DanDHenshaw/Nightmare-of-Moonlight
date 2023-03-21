using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private HealthSystem _healthSystem;

    [SerializeField] private Image[] _healthPoints;

    void Awake()
    {
        _healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFiller();
    }

    void HealthBarFiller()
    {
        for (int i = 0; i < _healthPoints.Length; i++)
        {
            _healthPoints[i].enabled = !DisplayHealthPoint(_healthSystem._health, i);
        }
    }

    bool DisplayHealthPoint(float health, int pointNumber)
    {
        return ((pointNumber * 10) >= health);
    }
}
