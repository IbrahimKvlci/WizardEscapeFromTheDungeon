using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasePlayerState : EnemyStateBase
{ 

    public EnemyChasePlayerState(Enemy enemy, IEnemyStateService enemyStateService) : base(enemy, enemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _enemy.EnemyMovementController.SetMovement(true);

    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.EnemyMovementController.HandleMovementToThePlayer();
        if (_enemy.EnemyTriggerController.IsPlayerTriggeredToBePreparedForAttack())
        {
            _enemyStateService.SwitchState(_enemy.EnemyAimState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _enemy.EnemyMovementController.SetMovement(false);
    }
}

