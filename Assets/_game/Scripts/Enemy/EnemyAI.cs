using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Transform _target;
    private HealthSystem _healthSystem;

    private SpriteRenderer _sprite;
    private Animator _animator;

    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Attacking")]
    [SerializeField] private float timeBetweenAttacks;
    private bool alreadyAttacked;
    [SerializeField] private float attackDamage;

    [Header("States")]
    [SerializeField] private float attackRange;
    [SerializeField] private bool playerInAttackRange;

    void Awake()
    {
        _target = GameObject.Find("Player").transform;
        _healthSystem = _target.gameObject.GetComponent<HealthSystem>();

        _agent = GetComponent<NavMeshAgent>();

        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInAttackRange) { ChasePlayer(); }
        else { AttackPlayer(); }

        // Flips Sprite
        _sprite.flipX = _target.transform.position.x > transform.position.x;

        // Check if moving
        if (Vector3.Distance(_agent.destination, _agent.transform.position) <= attackRange)
        {
            if (_agent.velocity.sqrMagnitude == 0f)
            {
                _animator.SetBool("Moving", false);
            }
        }
        else
        {
            _animator.SetBool("Moving", true);
        }
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_target.position);
    }

    private void AttackPlayer()
    {
        // Stop enemy moving
        _agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            // Enemy Attack
            _animator.SetTrigger("Attack");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void DamagePlayer()
    {
        if (playerInAttackRange)
        {
            _healthSystem.TakeDamage(attackDamage);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
