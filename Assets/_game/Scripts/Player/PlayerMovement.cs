using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerControlManager))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    [Tooltip("Player movement speed")] 
    [SerializeField] private float moveSpeed;

    [Header("Dash")]
    [Tooltip("Player dash speed")]
    [SerializeField] private float dashSpeed;
    private float dashTime;
    [Tooltip("Time player is dashing")]
    [SerializeField] private float startDashTime;
    private float dashCooldown;
    [Tooltip("Dash cooldown")]
    [SerializeField] private float startDashCooldown;

    private Vector3 direction;
    private bool isDashing = false;

    private Rigidbody _rigidbody;
    private PlayerControlManager _controlManager;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _controlManager = GetComponent<PlayerControlManager>();
    }

    void Update()
    {
        if (dashCooldown <= 0)
        {
            if (_controlManager.HasDashed())
            {
                isDashing = true;
                dashCooldown = startDashCooldown;
                dashTime = startDashTime;
            }
        }

        if (dashTime <= 0) isDashing = false;

        dashCooldown -= Time.deltaTime;
        dashTime -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        float speed = moveSpeed;

        if (isDashing)
        {
            speed = dashSpeed;
        }
        else
        {
            direction = _controlManager.Movement();
        }

        _rigidbody.velocity = direction * speed;
    }
}
