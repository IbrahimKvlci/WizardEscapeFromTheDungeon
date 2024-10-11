using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitchEnemyVisual : BasicEnemyVisual
{
    [SerializeField] private LitchEnemy litchEnemy;
    [SerializeField] private Animator animator;

    enum LitchEnemyAnimationEnum
    {
        IsRunning,
        AttackTrigger,
        IsDead,
    }

    private void Start()
    {
        ((EnemyDeathState)litchEnemy.EnemyDeathState).OnEnemyDead += LitchEnemyVisual_OnEnemyDead;
        litchEnemy.EnemyAttackController.OnAttackStarted += EnemyAttackController_OnAttackStarted;
        litchEnemy.EnemyMovementController.OnEnemyMovementChanged += EnemyMovementController_OnEnemyMovementChanged;
    }
    private void OnDisable()
    {
        ((EnemyDeathState)litchEnemy.EnemyDeathState).OnEnemyDead -= LitchEnemyVisual_OnEnemyDead;
        litchEnemy.EnemyAttackController.OnAttackStarted -= EnemyAttackController_OnAttackStarted;
        litchEnemy.EnemyMovementController.OnEnemyMovementChanged -= EnemyMovementController_OnEnemyMovementChanged;
    }

    private void EnemyMovementController_OnEnemyMovementChanged(object sender, System.EventArgs e)
    {
        SetBoolAnim(LitchEnemyAnimationEnum.IsRunning, litchEnemy.EnemyMovementController.CanMove);
    }

    private void EnemyAttackController_OnAttackStarted(object sender, System.EventArgs e)
    {
        TriggerAnim(LitchEnemyAnimationEnum.AttackTrigger);
    }

    private void LitchEnemyVisual_OnEnemyDead(object sender, System.EventArgs e)
    {
        SetBoolAnim(LitchEnemyAnimationEnum.IsDead,true);
    }

    private void SetBoolAnim(LitchEnemyAnimationEnum litchEnemyAnimationEnum,bool value)
    {
        animator.SetBool(litchEnemyAnimationEnum.ToString(),value);
    }
    private void TriggerAnim(LitchEnemyAnimationEnum litchEnemyAnimationEnum)
    {
        animator.SetTrigger(litchEnemyAnimationEnum.ToString());
    }
}
