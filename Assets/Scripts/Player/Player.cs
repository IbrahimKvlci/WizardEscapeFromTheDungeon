using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField] public PlayerMovementController PlayerMovementController {  get; set; }
    [field:SerializeField] public Rigidbody Rigidbody { get; set; }
    [field:SerializeField] public PlayerVisualController PlayerVisualController { get; set; }
    [field: SerializeField] public PlayerAttackController PlayerAttackController { get; set; }


    public IPlayerState PlayerIdleState { get; set; }
    public IPlayerState PlayerRunningState { get; set; }
    public IPlayerState PlayerAirborneState { get; set; }

    private IPlayerStateService _playerStateService;

    public static Player Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        _playerStateService = new PlayerStateManger();

        PlayerIdleState = new PlayerIdleState(this,_playerStateService);
        PlayerRunningState = new PlayerRunningState(this, _playerStateService);
        PlayerAirborneState = new PlayerAirborneState(this, _playerStateService);

    }
}
