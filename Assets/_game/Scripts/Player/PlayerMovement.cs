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
    [Tooltip("Time player is dashing")]
    [SerializeField] private float startDashTime;
    [Tooltip("Dash cooldown")]
    [SerializeField] private float startDashCooldown;

    private Vector3 direction;
    private bool isDashing = false;
    private bool dashCooldown = false;

    private Rigidbody _rigidbody;
    private PlayerControlManager _controlManager;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _controlManager = GetComponent<PlayerControlManager>();
    }

    void Update()
    {
        if (!dashCooldown)
        {
            if (_controlManager.HasDashed())
            {
                isDashing = true;
                dashCooldown = true;
                
                Invoke(nameof(ResetDash), startDashTime);
                Invoke(nameof(ResetDashCooldown), startDashCooldown);
            }
        }
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

    void ResetDash()
    {
        isDashing = false;
    }

    void ResetDashCooldown()
    {
        dashCooldown = false;
    }
}
