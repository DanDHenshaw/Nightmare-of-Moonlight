using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerControlManager))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    [Tooltip("Player movement speed")] 
    [SerializeField] private float moveSpeed;

    [System.Serializable]
    private struct FootstepPitch
    {
        public float low;
        public float high;
    }
    [SerializeField] private FootstepPitch footstepPitch;
     
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
    private bool _isMoving = false;

    private Rigidbody _rigidbody;
    private PlayerControlManager _controlManager;

    private Animator _animator;

    private WeaponSystem _weaponSystem;

    private AudioSource _audioSource;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _controlManager = GetComponent<PlayerControlManager>();

        _animator = GetComponent<Animator>();

        _audioSource = GetComponentInChildren<AudioSource>();

        ReplaceWeapon();
    }

    void Update()
    {
        if (!_dashCooldown)
        {
            if (_controlManager.isDashing)
            {
                _isDashing = true;
                _dashCooldown = true;
                
                Invoke(nameof(ResetDash), startDashTime);
                Invoke(nameof(ResetDashCooldown), startDashCooldown);
            }
        }

        if (_weaponSystem)
        {
            _weaponSystem.isFacingLeft = _isFacingLeft;
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
            if (move.x < 0)
            {
                _isFacingLeft = true;
                _isMoving = true;
            }
            if (move.x > 0)
            {
                _isFacingLeft = false;
                _isMoving = true;
            }

            if (move.z < 0)
            {
                _isFacingLeft = false;
                _isMoving = true;
            }
            if (move.z > 0)
            {
                _isFacingLeft = false;
                _isMoving = true;
            }

            if (move.x < 0 && move.z > 0 || move.x < 0 && move.z < 0)
            {
                _isFacingLeft = true;
                _isMoving = true;
            } 
            if (move.x > 0 && move.z > 0 || move.x > 0 && move.z < 0)
            {
                _isFacingLeft = false;
                _isMoving = true;
            }
        }
        else
        {
            _isMoving = false;
        }

        _animator.SetBool("isFacingLeft", _isFacingLeft);
        _animator.SetBool("isMoving", _isMoving);
    }

    void FootstepSound()
    {
        _audioSource.pitch = Random.Range(footstepPitch.low, footstepPitch.high);
        _audioSource.Play();
    }

    public void ReplaceWeapon()
    {
        _weaponSystem = GetComponentInChildren<WeaponSystem>();
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
