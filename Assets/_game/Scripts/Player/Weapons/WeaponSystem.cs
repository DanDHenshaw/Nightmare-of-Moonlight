using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private WeaponData _data;

    private PlayerControlManager _controlManager;
    private Animator _animator;

    [HideInInspector] public bool isFacingLeft = false;

    private bool _attackCooldown = false;

    [SerializeField] private LayerMask _enemyMask;

    void Awake()
    {
        _controlManager = GetComponentInParent<PlayerControlManager>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetBool("isFacingLeft", isFacingLeft);

        if (!_attackCooldown)
        {
            if (_controlManager.isAttacking)
            {
                _attackCooldown = true;

                _animator.SetTrigger("Attack");

                Invoke(nameof(ResetAttackCooldown), _data.attackCooldown);
            }
        }
    }

    void DamageEnemy()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, _data.range, _enemyMask);

        foreach (Collider collider in enemyColliders)
        {
            HealthSystem enemyHealth = collider.gameObject.GetComponent<HealthSystem>();
            enemyHealth.TakeDamage(_data.damage);
        }
    }

    void ResetAttackCooldown()
    {
        _attackCooldown = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _data.range);
    }
}
