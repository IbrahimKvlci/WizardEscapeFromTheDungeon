using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyStateBase : IRockGolemBossEnemyState
{
    protected RockGolemBoss _rockGolemBoss;
    protected IRockGolemBossEnemyStateService _rockGolemBossEnemyStateService;

    public bool CanChangeState { get; set; } = true;

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
        if(_rockGolemBossEnemyStateService.CurrentState is not RockGolemBossEnemyEarthquakeState)
        {
            if (_rockGolemBoss.EarthquakeTimer >= 15)
            {
                _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.EarthquakeState);
            }
        }
    }
}
