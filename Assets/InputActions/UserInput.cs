using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class UserInput : MonoBehaviour
{
    public static UserInput instance;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool FireInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool DashInput { get; private set; }
    private PlayerInput _playerInput;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        UpdateInputs();
    }
    private void UpdateInputs()
    {
        MoveInput = _playerInput.actions["Move"].ReadValue<Vector2>();
        LookInput = _playerInput.actions["Look"].ReadValue<Vector2>();
        FireInput = _playerInput.actions["Fire"].WasPressedThisFrame();
        JumpInput = _playerInput.actions["Jump"].IsPressed();
        JumpReleased = _playerInput.actions["Jump"].WasReleasedThisFrame();
        DashInput = _playerInput.actions["Dash"].WasPressedThisFrame();
    }
}
