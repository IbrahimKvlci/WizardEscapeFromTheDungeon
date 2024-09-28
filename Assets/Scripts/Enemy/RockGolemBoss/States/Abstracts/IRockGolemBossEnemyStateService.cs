using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRockGolemBossEnemyStateService
{
    public IRockGolemBossEnemyState CurrentState { get; set; }

    void SwitchState(IRockGolemBossEnemyState state);
    void Initialize(IRockGolemBossEnemyState state);
}
