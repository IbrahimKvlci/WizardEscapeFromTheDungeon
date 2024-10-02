using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStateBase
{
    //public event EventHandler OnAttackStarted;
    //public event EventHandler OnAttackFinished;

    private bool isAttacking;

    public EnemyAttackState(BasicEnemy enemy, IEnemyStateService enemyStateService) : base(enemy, enemyStateService)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        //OnAttackStarted?.Invoke(this, EventArgs.Empty);
        _enemy.BasicEnemyVisual.OnAttackStarted += BasicEnemyVisual_OnAttackStarted;
        _enemy.BasicEnemyVisual.OnAttackFinished += BasicEnemyVisual_OnAttackFinished;
        _enemy.BasicEnemyVisual.OnAnimationFinished += BasicEnemyVisual_OnAnimationFinished;

        _enemy.EnemyAttackController.AttackStarted();
        isAttacking = false;
    }

    private void BasicEnemyVisual_OnAnimationFinished(object sender, EventArgs e)
    {
        _enemyStateService.SwitchState(_enemy.EnemyAimState);
    }

    private void BasicEnemyVisual_OnAttackFinished(object sender, EventArgs e)
    {
        isAttacking = false;
    }

    private void BasicEnemyVisual_OnAttackStarted(object sender, EventArgs e)
    {
        isAttacking = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(isAttacking)
        {
            if (_enemy.EnemyTriggerController.EnemyTriggerDetector.IsEnemyTriggeredToBeAttacked())
            {
                Player.Instance.PlayerHealth.TakeDamage(_enemy.EnemySO.enemyDamage);
                isAttacking = false;
            }
        }

        Debug.Log(isAttacking);
    }

    public override void ExitState()
    {
        base.ExitState();
        //OnAttackFinished?.Invoke(this, EventArgs.Empty);
        _enemy.EnemyAttackController?.AttackFinished();

        _enemy.BasicEnemyVisual.OnAttackStarted -= BasicEnemyVisual_OnAttackStarted;
        _enemy.BasicEnemyVisual.OnAttackFinished -= BasicEnemyVisual_OnAttackFinished;
        _enemy.BasicEnemyVisual.OnAnimationFinished -= BasicEnemyVisual_OnAnimationFinished;

    }
}
