using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event EventHandler OnStunChanged;

    [field: SerializeField] public PlayerMovementController PlayerMovementController { get; set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; set; }
    [field: SerializeField] public PlayerVisualController PlayerVisualController { get; set; }
    [field: SerializeField] public PlayerAttackController PlayerAttackController { get; set; }
    [field: SerializeField] public Dashing Dashing { get; set; }

    private bool _isStunned;
    public bool IsStunned
    {
        get
        {
            return _isStunned;
        }
        set
        {
            _isStunned = value;
            OnStunChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public IPlayerState PlayerIdleState { get; set; }
    public IPlayerState PlayerRunningState { get; set; }
    public IPlayerState PlayerAirborneState { get; set; }

    private IPlayerStateService _playerStateService;

    public static Player Instance { get; set; }

    public PlayerStateEnum State { get; set; }
    public enum PlayerStateEnum
    {
        Moving,
        Attacking,
        Dashing,
    }

    private void Awake()
    {
        Instance = this;

        _playerStateService = new PlayerStateManger();

        PlayerIdleState = new PlayerIdleState(this, _playerStateService);
        PlayerRunningState = new PlayerRunningState(this, _playerStateService);
        PlayerAirborneState = new PlayerAirborneState(this, _playerStateService);

    }

    private void Start()
    {
        IsStunned = false;
    }

    private void Update()
    {
        ////Moving
        //if (PlayerMovementController.IsMoving)
        //{
        //    State=PlayerStateEnum.Moving;
        //}
        //else if (PlayerAttackController.IsAttacking)
        //{
        //    State = PlayerStateEnum.Attacking;
        //}
        //else if (Dashing.IsDashing)
        //{
        //    State = PlayerStateEnum.Dashing;
        //}

        #region StunHandle
        if (IsStunned)
        {
            StunPlayer();
        }
        else
        {
            ClearStun();
        }

        #endregion
    }

    private void StunPlayer()
    {
        PlayerMovementController.CanMove = false;
        PlayerAttackController.CanAttack = false;
    }
    private void ClearStun()
    {
        PlayerMovementController.CanMove = true;
        PlayerAttackController.CanAttack = true;
    }
}
