using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase : IEnemyState
{
    protected BasicEnemy _enemy;
    protected IEnemyStateService _enemyStateService;

    public EnemyStateBase(BasicEnemy enemy,IEnemyStateService enemyStateService)
    {
        _enemy = enemy;
        _enemyStateService = enemyStateService;
    }

    public virtual void EnterState()
    {
    }

    public virtual void ExitState()
    {
    }

    public virtual void UpdateState()
    {
        if (_enemy.EnemyHealth.IsDead&&_enemyStateService.CurrentEnemyState is not EnemyDeathState)
        {
            _enemyStateService.SwitchState(_enemy.EnemyDeathState);
        }
    }
}
