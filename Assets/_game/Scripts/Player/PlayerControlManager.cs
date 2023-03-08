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

    public Vector3 Movement()
    {
        Vector2 move = _controls.Player.Movement.ReadValue<Vector2>();
        return new Vector3(move.x, 0, move.y);
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
