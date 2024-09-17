using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStateBase
{
    private float timer;

    public EnemyAttackState(Enemy enemy, IEnemyStateService enemyStateService) : base(enemy, enemyStateService)
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
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
