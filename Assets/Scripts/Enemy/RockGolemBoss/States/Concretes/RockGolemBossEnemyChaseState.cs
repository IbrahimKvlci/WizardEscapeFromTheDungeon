using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyChaseState : RockGolemBossEnemyStateBase
{
    public RockGolemBossEnemyChaseState(RockGolemBoss rockGolemBoss, IRockGolemBossEnemyStateService rockGolemBossEnemyStateService) : base(rockGolemBoss, rockGolemBossEnemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _rockGolemBoss.EnemyMovementController.SetMovement(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        HandleMovement();

        if (_rockGolemBoss.EnemyTriggerController.IsPlayerTriggeredToBePreparedForAttack())
        {
            _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.PunchState);
        }
        else
        {
            _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.ThrowRockState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _rockGolemBoss.EnemyMovementController.SetMovement(false);

    }

    private void HandleMovement()
    {
        _rockGolemBoss.EnemyMovementController.HandleMovementToThePlayer();
    }
}
