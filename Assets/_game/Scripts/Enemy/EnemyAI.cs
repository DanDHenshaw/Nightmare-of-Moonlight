using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Transform _target;
    private HealthSystem _healthSystem;

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
    }

    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInAttackRange) { ChasePlayer(); }
        else { AttackPlayer(); }
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
            _healthSystem.TakeDamage(attackDamage);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
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
