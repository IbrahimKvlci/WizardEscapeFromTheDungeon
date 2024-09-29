using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRockGolemBossEnemyState
{
    public bool CanChangeState { get; set; }

    void EnterState();
    void UpdateState();
    void ExitState();
}
