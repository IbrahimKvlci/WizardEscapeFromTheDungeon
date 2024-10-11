using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public Action IdleEnemyAction { get; set; }

    [field:SerializeField] public BasicEnemyVisual BasicEnemyVisual {  get; set; }

    public IEnemyState EnemyIdleState { get; set; }
    public IEnemyState EnemyChaseState { get; set; }
    public IEnemyState EnemyAimState { get; set; }
    public IEnemyState EnemyAttackState { get; set; }
    public IEnemyState EnemyDeathState { get; set; }

    private IEnemyStateService _enemyStateService;


    protected override void Awake()
    {
        base.Awake();

        _enemyStateService = new EnemyStateManager();

        EnemyIdleState = new EnemyIdleState(this, _enemyStateService);
        EnemyChaseState = new EnemyChasePlayerState(this, _enemyStateService);
        EnemyAimState = new EnemyAimState(this, _enemyStateService);
        EnemyAttackState = new EnemyAttackState(this, _enemyStateService);
        EnemyDeathState = new EnemyDeathState(this, _enemyStateService);
    }


    protected override void Start()
    {

        _enemyStateService.Initialize(EnemyIdleState);
    }

    protected override void Update()
    {
        base.Update();
        _enemyStateService.CurrentEnemyState.UpdateState();

    }

    public virtual void Attack(out bool isAttacking)
    {
        isAttacking = false;
        Debug.LogError("Base class attack method");
    }
}
