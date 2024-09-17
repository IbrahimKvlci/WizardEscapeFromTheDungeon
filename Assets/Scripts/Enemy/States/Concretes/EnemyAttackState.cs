using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStateBase
{
    public event EventHandler OnAttackStarted;
    public event EventHandler OnAttackFinished;

    private float timer;
    private bool isAttacked;

    public EnemyAttackState(Enemy enemy, IEnemyStateService enemyStateService) : base(enemy, enemyStateService)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        OnAttackStarted?.Invoke(this, EventArgs.Empty);
        timer = 0;
        isAttacked = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_enemy.EnemyTriggerController.EnemyTriggerDetector.IsEnemyTriggeredToBeAttacked()&&!isAttacked)
        {
            Debug.Log("Attack");
            isAttacked= true;
        }

        timer += Time.deltaTime;
        if(timer>=2)
            _enemyStateService.SwitchState(_enemy.EnemyAimState);
    }

    public override void ExitState()
    {
        base.ExitState();
        OnAttackFinished?.Invoke(this, EventArgs.Empty);
    }
}
