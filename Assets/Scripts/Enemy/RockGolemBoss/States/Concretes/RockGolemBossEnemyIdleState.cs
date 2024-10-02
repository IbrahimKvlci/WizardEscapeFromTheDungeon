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
        CanChangeState = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_rockGolemBoss.EnemyAttackController.CanAttack)
        {
            if (_rockGolemBoss.EnemyTriggerController.IsPlayerTriggeredToBePreparedForAttack())
            {
                _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.PunchState);
            }
            else if (_rockGolemBoss.ThrowRockTimer >= 20 && Vector3.Distance(_rockGolemBoss.transform.position, Player.Instance.transform.position) >= 0)
            {
                _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.ThrowRockState);
            }
            else
            {
                _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.ChaseState);
            }
        }
    }
}
