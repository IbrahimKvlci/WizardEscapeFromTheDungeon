using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Action IdleEnemyAction {  get; set; }

    [field:SerializeField] public EnemySO EnemySO {  get; private set; }
    [field: SerializeField] public EnemyHealth EnemyHealth {  get; set; }
    [field:SerializeField] public EnemyAttackController EnemyAttackController { get; set; }
    [field: SerializeField] public EnemyMovementController EnemyMovementController { get; set; }

    public IEnemyState EnemyIdleState { get; set; }
    public IEnemyState EnemyChaseState { get; set; }
    public IEnemyState EnemyAimState { get; set; }
    public IEnemyState EnemyAttackState { get; set; }

    private IEnemyStateService _enemyStateService;

    protected virtual void Awake()
    {
        _enemyStateService = new EnemyStateManager();

        

    }

    private void Start()
    {
        EnemyIdleState = new EnemyIdleState(this, _enemyStateService);
        EnemyChaseState = new EnemyChasePlayerState(this, _enemyStateService);
        EnemyAimState = new EnemyAimState(this, _enemyStateService);
        EnemyAttackState = new EnemyAttackState(this, _enemyStateService);
        _enemyStateService.Initialize(EnemyIdleState);
    }

    private void Update()
    {
        _enemyStateService.CurrentEnemyState.UpdateState();
    }

}
