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

    private void Awake()
    {
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

    private void IdleSleep()
    {
        Debug.Log("Sleeping");
    }
    private void IdleStand()
    {
        Debug.Log("Stay Stand");
    }
}
