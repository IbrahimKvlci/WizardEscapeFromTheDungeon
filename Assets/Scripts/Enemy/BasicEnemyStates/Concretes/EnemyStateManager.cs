using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : IEnemyStateService
{
    public IEnemyState CurrentEnemyState { get; set ; }

    public void Initialize(IEnemyState enemyState)
    {
        CurrentEnemyState = enemyState;
        CurrentEnemyState.EnterState();
    }

    public void SwitchState(IEnemyState enemyState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = enemyState;
        CurrentEnemyState.EnterState();
    }
}
