using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyStateBase : IRockGolemBossEnemyState
{
    protected RockGolemBoss _rockGolemBoss;
    protected IRockGolemBossEnemyStateService _rockGolemBossEnemyStateService;

    public RockGolemBossEnemyStateBase(RockGolemBoss rockGolemBoss, IRockGolemBossEnemyStateService rockGolemBossEnemyStateService)
    {
        _rockGolemBoss = rockGolemBoss;
        _rockGolemBossEnemyStateService=rockGolemBossEnemyStateService; 
    }

    public virtual void EnterState()
    {
    }

    public virtual void ExitState()
    {
    }

    public virtual void UpdateState()
    {
    }
}
