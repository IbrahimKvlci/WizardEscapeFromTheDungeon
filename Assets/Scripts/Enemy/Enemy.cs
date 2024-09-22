using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Action IdleEnemyAction { get; set; }

    [field: SerializeField] public EnemySO EnemySO { get; private set; }
    [field: SerializeField] public EnemyHealth EnemyHealth { get; set; }
    [field: SerializeField] public EnemyAttackController EnemyAttackController { get; set; }
    [field: SerializeField] public EnemyMovementController EnemyMovementController { get; set; }
    [field: SerializeField] public EnemyTriggerController EnemyTriggerController { get; set; }

    [SerializeField] private Transform holdableObjectList;


    public IEnemyState EnemyIdleState { get; set; }
    public IEnemyState EnemyChaseState { get; set; }
    public IEnemyState EnemyAimState { get; set; }
    public IEnemyState EnemyAttackState { get; set; }
    public IEnemyState EnemyDeathState { get; set; }


    private IEnemyStateService _enemyStateService;

    protected virtual void Awake()
    {
        _enemyStateService = new EnemyStateManager();

        EnemyIdleState = new EnemyIdleState(this, _enemyStateService);
        EnemyChaseState = new EnemyChasePlayerState(this, _enemyStateService);
        EnemyAimState = new EnemyAimState(this, _enemyStateService);
        EnemyAttackState = new EnemyAttackState(this, _enemyStateService);
        EnemyDeathState = new EnemyDeathState(this, _enemyStateService);
    }

    private void Start()
    {

        _enemyStateService.Initialize(EnemyIdleState);
    }

    private void Update()
    {
        _enemyStateService.CurrentEnemyState.UpdateState();
    }

    public void DropHoldableObjectsOnEnemy()
    {

        foreach (Transform holdableObject in holdableObjectList)
        {
            holdableObject.gameObject.GetComponent<IHoldable>().Drop();
        }
    }

}
