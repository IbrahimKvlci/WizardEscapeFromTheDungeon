using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager :MonoBehaviour, IInputService
{
    private InputActions inputActions;

    public static InputManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        inputActions=new InputActions();

        inputActions.Enable();
    }

    public bool CheckJumpButton()
    {
        return inputActions.Player.Jump.WasPressedThisFrame();
    }

    public Vector2 GetMovementVector()
    {
        return inputActions.Player.Movement.ReadValue<Vector2>();
    }
}
