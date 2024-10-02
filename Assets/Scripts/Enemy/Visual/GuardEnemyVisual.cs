using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemyVisual : BasicEnemyVisual
{
    [SerializeField] private GuardEnemy enemy;
    [SerializeField] private Animator animator;

    enum GuardEnemyAnimationEnum
    {
        IsSleeping,
        IsRunning,
        AttackTrigger,
        IsDead,
    }

    private void Start()
    {
        enemy.OnSleepingChanged += Enemy_OnSleepingChanged;
        enemy.EnemyAttackController.OnAttackStarted += GuardEnemyVisual_OnAttack;
        ((EnemyDeathState)enemy.EnemyDeathState).OnEnemyDead += GuardEnemyVisual_OnEnemyDead;
        enemy.EnemyMovementController.OnEnemyMovementChanged += EnemyMovementController_OnEnemyMovementChanged;

        SetBool(GuardEnemyAnimationEnum.IsSleeping, enemy.IsSleeping);
    }

    private void GuardEnemyVisual_OnEnemyDead(object sender, System.EventArgs e)
    {
        SetBool(GuardEnemyAnimationEnum.IsDead, true);
    }

    private void EnemyMovementController_OnEnemyMovementChanged(object sender, System.EventArgs e)
    {
        SetBool(GuardEnemyAnimationEnum.IsRunning, enemy.EnemyMovementController.CanMove);
    }

    private void GuardEnemyVisual_OnAttack(object sender, System.EventArgs e)
    {
        TriggerAnimation(GuardEnemyAnimationEnum.AttackTrigger);
    }

    private void OnDisable()
    {
        enemy.OnSleepingChanged -= Enemy_OnSleepingChanged;
        enemy.EnemyAttackController.OnAttackStarted -= GuardEnemyVisual_OnAttack;
        ((EnemyDeathState)enemy.EnemyDeathState).OnEnemyDead -= GuardEnemyVisual_OnEnemyDead;
        enemy.EnemyMovementController.OnEnemyMovementChanged -= EnemyMovementController_OnEnemyMovementChanged;

    }

    private void Enemy_OnSleepingChanged(object sender, System.EventArgs e)
    {
        SetBool(GuardEnemyAnimationEnum.IsSleeping, enemy.IsSleeping);
    }

    private void SetBool(GuardEnemyAnimationEnum guardEnemyAnimationEnum, bool value)
    {
        animator.SetBool(guardEnemyAnimationEnum.ToString(), value);
    }
    private void TriggerAnimation(GuardEnemyAnimationEnum guardEnemyAnimationEnum)
    {
        animator.SetTrigger(guardEnemyAnimationEnum.ToString());
    }
}
