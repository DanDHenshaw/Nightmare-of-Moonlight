using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    private Controls _controls;

    private bool isDashing = false;

    void Awake()
    {
        _controls = new Controls();

        _controls.Player.Dash.performed += ctx => isDashing = true;
        _controls.Player.Dash.canceled += ctx => isDashing = false;
    }

    public Vector2 Movement()
    {
        Vector2 move = _controls.Player.Movement.ReadValue<Vector2>();

        return _controls.Player.Movement.ReadValue<Vector2>();
    }

    public bool HasDashed()
    {
        return isDashing;
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }
}
