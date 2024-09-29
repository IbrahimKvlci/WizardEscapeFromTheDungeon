using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossVisual : MonoBehaviour
{
    public event EventHandler OnGetRock;
    public event EventHandler OnThrowRock;
    public event EventHandler OnThrowingFinished;

    [SerializeField] private RockGolemBoss rockGolemBoss;
    [SerializeField] private Animator animator;

    enum RockGolemBossAnimationEnum
    {
        IsWalking,
        PunchTrigger,
        ThrowRockTrigger,
        EarthquakeTrigger,
        IsDead,
    }

    private void Start()
    {
        rockGolemBoss.EnemyMovementController.OnEnemyMovementChanged += EnemyMovementController_OnEnemyMovementChanged;
        rockGolemBoss.EnemyAttackController.OnAttackStarted += EnemyAttackController_OnAttackStarted;
        ((RockGolemBossEnemyThrowRockState)rockGolemBoss.ThrowRockState).OnThrowingRock += RockGolemBossVisual_OnThrowingRock;
        ((RockGolemBossEnemyEarthquakeState)rockGolemBoss.EarthquakeState).OnEarthquake += RockGolemBossVisual_OnEarthquake;
    }

    private void OnDisable()
    {
        rockGolemBoss.EnemyMovementController.OnEnemyMovementChanged -= EnemyMovementController_OnEnemyMovementChanged;
        rockGolemBoss.EnemyAttackController.OnAttackStarted -= EnemyAttackController_OnAttackStarted;
        ((RockGolemBossEnemyThrowRockState)rockGolemBoss.ThrowRockState).OnThrowingRock -= RockGolemBossVisual_OnThrowingRock;
        ((RockGolemBossEnemyEarthquakeState)rockGolemBoss.EarthquakeState).OnEarthquake -= RockGolemBossVisual_OnEarthquake;
    }

    private void RockGolemBossVisual_OnEarthquake(object sender, System.EventArgs e)
    {
        TriggerAnimation(RockGolemBossAnimationEnum.EarthquakeTrigger);
    }

    private void RockGolemBossVisual_OnThrowingRock(object sender, System.EventArgs e)
    {
        TriggerAnimation(RockGolemBossAnimationEnum.ThrowRockTrigger);
    }

    private void EnemyAttackController_OnAttackStarted(object sender, System.EventArgs e)
    {
        TriggerAnimation(RockGolemBossAnimationEnum.PunchTrigger);
    }

    private void EnemyMovementController_OnEnemyMovementChanged(object sender, System.EventArgs e)
    {
        SetBoolAnimation(RockGolemBossAnimationEnum.IsWalking,rockGolemBoss.EnemyMovementController.CanMove);
    }

    private void TriggerAnimation(RockGolemBossAnimationEnum rockGolemBossAnimationEnum)
    {
        animator.SetTrigger(rockGolemBossAnimationEnum.ToString());
    }
    private void SetBoolAnimation(RockGolemBossAnimationEnum rockGolemBossAnimationEnum, bool value)
    {
        animator.SetBool(rockGolemBossAnimationEnum.ToString(), value);
    }

    private void GetRock()
    {
        OnGetRock?.Invoke(this, EventArgs.Empty);
    }
    private void ThrowRock()
    {
        OnThrowRock?.Invoke(this, EventArgs.Empty);
    }
    private void ThrowingFinished()
    {
        OnThrowingFinished?.Invoke(this, EventArgs.Empty);
    }
}