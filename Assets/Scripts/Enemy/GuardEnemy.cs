using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemy : Enemy
{
    public event EventHandler OnSleepingChanged;

    private bool _isSleeping = false;
    public bool IsSleeping
    {
        get { return _isSleeping; }
        set { _isSleeping = value; OnSleepingChanged?.Invoke(this, EventArgs.Empty); }
    }

    [field:SerializeField] public EnemyIdleEnum EnemyIdle { get; set; }
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

    private void Update()
    {
        if (EnemyAttackController.CanAttack)
        {
            IsSleeping = false;
        }
    }

    private void IdleSleep()
    {
        Debug.Log("Sleeping");
        if (!IsSleeping)
        {
            IdleEnemyAction = IdleStand;
        }
    }
    
    private void IdleStand()
    {
        Debug.Log("Stay Stand");
    }
}
