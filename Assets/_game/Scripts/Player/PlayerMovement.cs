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

    private Vector2 _direction;
    private bool _isDashing = false;
    private bool _dashCooldown = false;

    private Rigidbody _rigidbody;
    private PlayerControlManager _controlManager;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _controlManager = GetComponent<PlayerControlManager>();
    }

    void Update()
    {
        if (!_dashCooldown)
        {
            if (_controlManager.HasDashed())
            {
                _isDashing = true;
                _dashCooldown = true;
                
                Invoke(nameof(ResetDash), startDashTime);
                Invoke(nameof(ResetDashCooldown), startDashCooldown);
            }
        }
    }

    void FixedUpdate()
    {
        float speed = moveSpeed;

        if (_isDashing)
        {
            speed = dashSpeed;
        }
        else
        {
            _direction = _controlManager.Movement();
        }

        Vector3 move = transform.forward * _direction.y * speed + transform.right * _direction.x * speed;
        _rigidbody.velocity = move;
    }

    void ResetDash()
    {
        _isDashing = false;
    }

    void ResetDashCooldown()
    {
        _dashCooldown = false;
    }
}
