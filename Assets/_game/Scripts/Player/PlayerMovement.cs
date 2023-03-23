using Newtonsoft.Json.Bson;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerControlManager))]
[RequireComponent(typeof(Animator))]
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

    private bool _isFacingLeft = false;

    private Rigidbody _rigidbody;
    private PlayerControlManager _controlManager;

    private Animator _animator;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _controlManager = GetComponent<PlayerControlManager>();

        _animator = GetComponent<Animator>();
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

        AnimatePlayer(move);
    }

    void AnimatePlayer(Vector3 move)
    {
        if(move.magnitude > 0)
        {
            if (move.x < 0 && move.z > 0 || move.x < 0 && move.z < 0)
            {
                _animator.SetBool("isFacingLeft", true);
                _animator.SetBool("isMoving", true);
            }

            if (move.x > 0 && move.z > 0 || move.x > 0 && move.z < 0)
            {
                _animator.SetBool("isFacingLeft", false);
                _animator.SetBool("isMoving", true);
            }
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
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
