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

    [field:Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Camera camera;
    private IPlayerMovementService _playerMovementService;
    private IInputService _inputService;

    [field:Header("Jumping")]
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

    [field:Header("Running")]
    [field:SerializeField] public float Speed {  get; set; }
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


    Vector3 previousPos, lastMoveDirection;
    public bool CanMove { get; set; }


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
        CanMove = true;
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            _playerMovementService.HandleMovement(player);
        }
        else
        {
            IsRunning = false;
        }

        HandlePlayerForward();

    }
    private void Update()
    {
        if (CanMove)
        {
            HandleJump();
            HandleDash();
        }
        if (player.Rigidbody.velocity.y < -1 && player.Rigidbody.velocity.y > -2)
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


    private void HandlePlayerForward()
    {
        if (IsRunning)
        {
            //Rotate Player

            Quaternion toRotation = camera.transform.rotation;
            toRotation.x = player.transform.rotation.x;
            toRotation.z=player.transform.rotation.z;
            player.transform.rotation=Quaternion.Lerp(player.transform.rotation,toRotation,Time.deltaTime*5);

            //Rotate Player Visual
            if (player.transform.position != previousPos)
            {
                lastMoveDirection = (player.transform.position - previousPos).normalized;
                previousPos = player.transform.position;
            }

            Quaternion toRotationVisual = Quaternion.LookRotation(lastMoveDirection, Vector3.up);
            toRotationVisual.x = 0;
            toRotationVisual.z=0;
            player.PlayerVisualController.transform.rotation = Quaternion.RotateTowards(player.PlayerVisualController.transform.rotation, toRotationVisual, 720 * Time.deltaTime);
        }
    }

    private void HandleDash()
    {
        if (_inputService.DashButtonPressed())
        {
            _playerMovementService.Dash(player,20,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Grounded=true;
        IsFalling=false;
        Debug.Log("fal");
    }


}
