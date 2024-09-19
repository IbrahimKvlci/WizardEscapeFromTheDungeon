using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager :MonoBehaviour, IInputService
{
    private InputActions inputActions;

    public event EventHandler<IInputService.OnSwitchMagicPressedEventArgs> OnSwitchMagicPressed;

    public static InputManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        inputActions=new InputActions();

        inputActions.Enable();

        inputActions.Player.SwitchMagic.performed += SwitchMagic_performed;
    }

    private void SwitchMagic_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSwitchMagicPressed?.Invoke(this, new IInputService.OnSwitchMagicPressedEventArgs { magicIndex=(int)inputActions.Player.SwitchMagic.ReadValue<float>()});
    }

    public bool CheckJumpButton()
    {
        return inputActions.Player.Jump.WasPressedThisFrame();
    }

    public Vector2 GetMovementVector()
    {
        return inputActions.Player.Movement.ReadValue<Vector2>();
    }

    public bool FireButtonPressed()
    {
        return inputActions.Player.Fire.WasPressedThisFrame();
    }

    public int GetMagicIndex()
    {
         return (int)inputActions.Player.SwitchMagic.ReadValueAsObject();
    }

    public bool DashButtonPressed()
    {
        return inputActions.Player.Dash.WasPressedThisFrame();
    }

    public int GetScrollYValueSign()
    {
        if(inputActions.Player.MovementOfHoldingObject.ReadValue<float>() > 0)
        {
            return 1;
        }
        else if (inputActions.Player.MovementOfHoldingObject.ReadValue<float>() < 0)
        {
            return -1;
        }
        return 0;
    }
}
