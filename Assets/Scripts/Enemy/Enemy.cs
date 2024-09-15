using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Action IdleEnemyAction;

    [field: SerializeField] public EnemyHealth EnemyHealth {  get; set; }

    public EnemyStateEnum State { get; set; }
    public enum EnemyStateEnum
    {
        Idle,
        AttackPlayer,
    }

    private void Start()
    {
        State=EnemyStateEnum.Idle;
    }

    private void Update()
    {
        StateHandler();
    }

    private void StateHandler()
    {
        switch (State)
        {
            case EnemyStateEnum.Idle:
                IdleEnemyAction();
                break;
            case EnemyStateEnum.AttackPlayer:
                break;
            default:
                break;
        }
    }
}
