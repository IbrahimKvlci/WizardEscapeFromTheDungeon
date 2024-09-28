using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRockGolemBossEnemyState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}
