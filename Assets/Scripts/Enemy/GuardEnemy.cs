using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemy : BasicEnemy
{
    public event EventHandler OnSleepingChanged;

    private bool _isSleeping = false;
    public bool IsSleeping
    {
        get { return _isSleeping; }
        set { _isSleeping = value; OnSleepingChanged?.Invoke(this, EventArgs.Empty); }
    }

    [field: SerializeField] public EnemyIdleEnum EnemyIdle { get; set; }
    public enum EnemyIdleEnum
    {
        Sleep,
        Stand,
    }

    protected override void Awake()
    {
        base.Awake();
        switch (EnemyIdle)
        {
            case EnemyIdleEnum.Sleep:
                IdleEnemyAction = IdleSleep;
                IsSleeping = true;
                break;
            case EnemyIdleEnum.Stand:
                IdleEnemyAction = IdleStand;
                break;
            default:
                break;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (EnemyAttackController.CanAttack)
        {
            IsSleeping = false;
        }
    }

    public override void Attack(out bool isAttacking)
    {
        isAttacking = true;
        if (this.EnemyTriggerController.EnemyTriggerDetector.IsEnemyTriggeredToBeAttacked())
        {
            Player.Instance.PlayerHealth.TakeDamage(this.EnemySO.enemyDamage);
            isAttacking = false;    
        }
    }

    private void IdleSleep()
    {
        if (!IsSleeping)
        {
            IdleEnemyAction = IdleStand;
        }
    }

    private void IdleStand()
    {
    }
}
