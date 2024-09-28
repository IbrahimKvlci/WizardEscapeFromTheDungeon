using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyStateManager : IRockGolemBossEnemyStateService
{
    public IRockGolemBossEnemyState CurrentState { get; set ; }

    public void Initialize(IRockGolemBossEnemyState state)
    {
        CurrentState = state;
        CurrentState.EnterState();
    }

    public void SwitchState(IRockGolemBossEnemyState state)
    {
        CurrentState.ExitState();
        CurrentState=state;
        CurrentState.EnterState();
    }
}
