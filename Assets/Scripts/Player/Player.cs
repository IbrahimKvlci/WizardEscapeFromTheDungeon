using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event EventHandler OnStunChanged;
    public event EventHandler OnWandActiveChanged;

    [field:Header("References")]
    [field: SerializeField] public PlayerMovementController PlayerMovementController { get; set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; set; }
    [field: SerializeField] public PlayerVisualController PlayerVisualController { get; set; }
    [field: SerializeField] public PlayerAttackController PlayerAttackController { get; set; }
    [field: SerializeField] public Dashing Dashing { get; set; }
    [field:SerializeField] public GameObject WandObject { get; set; }

    [field: Header("Settings")]
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
            SetStunPlayer(!_isStunned);
            OnStunChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    private bool _hasWand;
    public bool HasWand
    {
        get
        {
            return _hasWand;
        }
        set
        {
            _hasWand = value;
            WandObject.SetActive(value);
            OnWandActiveChanged?.Invoke(this, EventArgs.Empty); 
        }
    }

    [field:Header("States")]
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
        HasWand = WandObject.activeSelf;
    }

    private void Update()
    {
    }

    private void SetStunPlayer(bool value)
    {
        PlayerMovementController.CanMove = value;
        PlayerAttackController.CanAttack = value;
    }
}
