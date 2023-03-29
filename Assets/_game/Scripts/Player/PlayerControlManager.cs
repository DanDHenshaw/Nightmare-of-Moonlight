using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    private Controls _controls;

    private PauseMenu _pauseMenu;

    [HideInInspector] public bool isDashing { get; private set; } = false;
    [HideInInspector] public bool isAttacking {get; private set; } = false;

    void Awake()
    {
        _controls = new Controls();
        _pauseMenu = FindObjectOfType<PauseMenu>();

        _controls.Player.Dash.performed += ctx => isDashing = true;
        _controls.Player.Dash.canceled += ctx => isDashing = false;

        _controls.Player.Attack.performed += ctx => isAttacking = true;
        _controls.Player.Attack.canceled += ctx => isAttacking = false;

        _controls.InGame.Pause.performed += ctx => _pauseMenu.OpenPauseMenu();
    }

    public Vector2 Movement()
    {
        Vector2 move = _controls.Player.Movement.ReadValue<Vector2>();

        return _controls.Player.Movement.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
