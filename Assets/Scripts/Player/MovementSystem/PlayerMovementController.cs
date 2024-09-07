using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public event EventHandler OnRunningChanged;
    public event EventHandler OnGroundedChanged;
    public event EventHandler OnFallingChanged;
    public event EventHandler OnJump;

    [field:SerializeField] public float Speed {  get; set; }
    [field: SerializeField] public float JumpSpeed { get; set; }

    private bool _grounded;
    public bool Grounded
    {
        get
        {
            return _grounded;
        }
        set
        {
            _grounded = value;
            OnGroundedChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool _isRunning;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            OnRunningChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool _isFalling;
    public bool IsFalling
    {
        get
        {
            return _isFalling;
        }
        set
        {
            _isFalling = value;
            OnFallingChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    [SerializeField] private Player player;

    private IPlayerMovementService _playerMovementService;
    private IInputService _inputService;

    private void Awake()
    {
        _playerMovementService=InGameIoC.Instance.PlayerMovementService;
        _inputService=InGameIoC.Instance.InputService;
    }

    private void Start()
    {
        Grounded = true;
        IsRunning = false;
        IsFalling = false;
    }

    private void Update()
    {

        HandleJump();
        _playerMovementService.HandleMovement(player);

        //Grounded = player.Rigidbody.velocity.y ==0;
        if (player.Rigidbody.velocity.y < -1&&player.Rigidbody.velocity.y>-2)
        {
            IsFalling = true;
        }
    }

    private void HandleJump()
    {
        if (_inputService.CheckJumpButton() && player.PlayerMovementController.Grounded)
        {
            _playerMovementService.Jump(player);
            Grounded= false;
            OnJump?.Invoke(this,EventArgs.Empty);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Grounded=true;
        IsFalling=false;
        Debug.Log("fal");
    }


}
