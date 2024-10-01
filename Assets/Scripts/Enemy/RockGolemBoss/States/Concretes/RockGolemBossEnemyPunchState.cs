using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyPunchState : RockGolemBossEnemyStateBase
{
    private float timer,attackTimer;
    private bool isAttacking,attackAnimFinished;

    public RockGolemBossEnemyPunchState(RockGolemBoss rockGolemBoss, IRockGolemBossEnemyStateService rockGolemBossEnemyStateService) : base(rockGolemBoss, rockGolemBossEnemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _rockGolemBoss.RockGolemBossVisual.OnPunchStarted += RockGolemBossVisual_OnPunchStarted;
        _rockGolemBoss.RockGolemBossVisual.OnPunchFinished += RockGolemBossVisual_OnPunchFinished;
        _rockGolemBoss.RockGolemBossVisual.OnPunchAnimationFinished += RockGolemBossVisual_OnPunchAnimationFinished;

        timer = _rockGolemBoss.EnemySO.enemyAimAttackTimerMax;
        attackTimer = 0;
        CanChangeState = true;
        isAttacking = false;
        attackAnimFinished = true;
    }

    private void RockGolemBossVisual_OnPunchAnimationFinished(object sender, System.EventArgs e)
    {
        attackAnimFinished = true;
        CanChangeState = true;
        _rockGolemBoss.EnemyAttackController.AttackFinished();
    }

    private void RockGolemBossVisual_OnPunchFinished(object sender, System.EventArgs e)
    {
        isAttacking = false;
    }

    private void RockGolemBossVisual_OnPunchStarted(object sender, System.EventArgs e)
    {
        isAttacking = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_rockGolemBoss.EnemyTriggerController.IsPlayerTriggeredToBeAttacked())
        {
            //Player triggered
            if (timer >= _rockGolemBoss.EnemySO.enemyAimAttackTimerMax&&attackAnimFinished)
            {
                timer = 0;
                //Punch
                //_rockGolemBoss.StartCoroutine(Attack());
                CanChangeState = false;
                _rockGolemBoss.EnemyAttackController.AttackStarted();
                attackAnimFinished = false;
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

        if (isAttacking)
        {
            if (_rockGolemBoss.EnemyTriggerController.EnemyTriggerDetector.IsEnemyTriggeredToBeAttacked())
            {
                Debug.Log("attack");
                isAttacking = false;
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
