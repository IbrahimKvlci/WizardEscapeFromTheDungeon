using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyStateBase
{
    public event EventHandler OnEnemyDead;

    float timer;

    public EnemyDeathState(Enemy enemy, IEnemyStateService enemyStateService) : base(enemy, enemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        timer = 0;

        OnEnemyDead?.Invoke(this, EventArgs.Empty);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timer >= 3)
        {
            timer = 0;
            _enemy.EnemyHealth.DestroySelf();
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
