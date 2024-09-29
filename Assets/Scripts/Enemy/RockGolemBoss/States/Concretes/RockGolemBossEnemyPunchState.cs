using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyPunchState : RockGolemBossEnemyStateBase
{
    float timer;

    public RockGolemBossEnemyPunchState(RockGolemBoss rockGolemBoss, IRockGolemBossEnemyStateService rockGolemBossEnemyStateService) : base(rockGolemBoss, rockGolemBossEnemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        timer = _rockGolemBoss.EnemySO.enemyAimAttackTimerMax;

        CanChangeState = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_rockGolemBoss.EnemyTriggerController.IsPlayerTriggeredToBeAttacked())
        {
            //Player triggered
            if (timer >= _rockGolemBoss.EnemySO.enemyAimAttackTimerMax)
            {
                timer = 0;
                //Punch
                _rockGolemBoss.StartCoroutine(Attack());
                
            }
        }
        else
        {
            //Aim
            Vector3 enemyForwardVector = Player.Instance.transform.position - _rockGolemBoss.transform.position;
            _rockGolemBoss.transform.forward = Vector3.Slerp(_rockGolemBoss.transform.forward, enemyForwardVector, 0.05f);
        }
        timer += Time.deltaTime;

        if (!_rockGolemBoss.EnemyTriggerController.IsPlayerTriggeredToBePreparedForAttack())
        {
            _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.IdleState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private IEnumerator Attack()
    {
        CanChangeState = false;
        _rockGolemBoss.EnemyAttackController.AttackStarted();
        if (_rockGolemBoss.EnemyTriggerController.EnemyTriggerDetector.IsEnemyTriggeredToBeAttacked())
        {
            Debug.Log("attack");
        }
        yield return new WaitForSeconds(2);
        _rockGolemBoss.EnemyAttackController.AttackFinished();
        CanChangeState = true;
    }
}
