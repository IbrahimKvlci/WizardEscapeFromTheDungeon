using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimState : EnemyStateBase
{
    private float timer;
    public EnemyAimState(Enemy enemy, IEnemyStateService enemyStateService) : base(enemy, enemyStateService)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        timer = 0;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_enemy.EnemyTriggerController.IsPlayerTriggeredToBePreparedForAttack())
        {
            _enemyStateService.SwitchState(_enemy.EnemyChaseState);
        }

        if (timer >= _enemy.EnemySO.enemyAimAttackTimerMax)
        {
            if(_enemy.EnemyTriggerController.IsPlayerTriggeredToBeAttacked())
            {
                _enemyStateService.SwitchState(_enemy.EnemyAttackState);
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}
