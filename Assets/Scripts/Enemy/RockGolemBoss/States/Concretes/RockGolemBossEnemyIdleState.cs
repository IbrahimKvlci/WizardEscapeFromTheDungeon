using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyIdleState : RockGolemBossEnemyStateBase
{
    public RockGolemBossEnemyIdleState(RockGolemBoss rockGolemBoss, IRockGolemBossEnemyStateService rockGolemBossEnemyStateService) : base(rockGolemBoss, rockGolemBossEnemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.ChaseState);
    }
}
