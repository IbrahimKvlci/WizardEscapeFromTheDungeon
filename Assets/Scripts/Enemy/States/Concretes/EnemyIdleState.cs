using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyStateBase
{
    public EnemyIdleState(Enemy enemy, IEnemyStateService enemyStateService) : base(enemy, enemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.IdleEnemyAction();
        if (_enemy.EnemyAttackController.CanAttack)
        {
            _enemyStateService.SwitchState(_enemy.EnemyChaseState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
