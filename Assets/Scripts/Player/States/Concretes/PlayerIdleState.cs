using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public PlayerIdleState(Player player, IPlayerStateService playerStateService) : base(player, playerStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(!_player.PlayerMovementController.Grounded)
        {
            _playerStateService.SwitchState(_player.PlayerAirborneState);
        }
        else if(_player.PlayerMovementController.IsRunning)
        {
            _playerStateService.SwitchState(_player.PlayerRunningState);
        }
    }
}
