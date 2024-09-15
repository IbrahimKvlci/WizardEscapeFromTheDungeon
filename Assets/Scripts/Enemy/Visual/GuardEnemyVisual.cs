using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemyVisual : MonoBehaviour
{
    [SerializeField] private GuardEnemy enemy;
    [SerializeField] private Animator animator;

    enum GuardEnemyAnimationEnum
    {
        IsSleeping,
    }

    private void Start()
    {
        enemy.OnSleepingChanged += Enemy_OnSleepingChanged;

        SetBool(GuardEnemyAnimationEnum.IsSleeping, enemy.IsSleeping);
    }

    private void OnDisable()
    {
        enemy.OnSleepingChanged -= Enemy_OnSleepingChanged;

    }

    private void Enemy_OnSleepingChanged(object sender, System.EventArgs e)
    {
        SetBool(GuardEnemyAnimationEnum.IsSleeping, enemy.IsSleeping);
    }

    private void SetBool(GuardEnemyAnimationEnum guardEnemyAnimationEnum, bool value)
    {
        animator.SetBool(guardEnemyAnimationEnum.ToString(), value);
    }
}
