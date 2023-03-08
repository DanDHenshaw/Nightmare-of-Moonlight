using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerControlManager))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    [Tooltip("Player Movement Speed")] 
    [SerializeField] private float moveSpeed;

    [Header("Dash")]
    [Tooltip("Player Dash Speed")]
    [SerializeField] private float dashSpeed;
    private float dashCooldown;
    [Tooltip("Dash cooldown")]
    [SerializeField] private float startDashCooldown;
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
            }
        }

        dashCooldown -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        Vector3 move = _controlManager.Movement();

        float speed = moveSpeed;

        if (isDashing)
        {
            speed = dashSpeed;
            isDashing = false;
        }

        _rigidbody.velocity = move * speed;
    }
}
