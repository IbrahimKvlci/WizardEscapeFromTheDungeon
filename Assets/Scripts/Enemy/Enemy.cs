using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    [field: SerializeField] public EnemySO EnemySO { get; private set; }
    [field: SerializeField] public EnemyHealth EnemyHealth { get; set; }
    [field: SerializeField] public EnemyAttackController EnemyAttackController { get; set; }
    [field: SerializeField] public EnemyMovementController EnemyMovementController { get; set; }
    [field: SerializeField] public EnemyTriggerController EnemyTriggerController { get; set; }

    [SerializeField] private Transform holdableObjectList;


    private bool _isStunned;
    public bool IsStunned
    {
        get
        {
            return _isStunned;
        }
        set
        {
            _isStunned = value;

            SetStunEnemy(value);
        }
    }



    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {

    }
    

    protected virtual void Update()
    {
    }

    public void DropHoldableObjectsOnEnemy()
    {

        foreach (Transform holdableObject in holdableObjectList)
        {
            holdableObject.gameObject.GetComponent<IHoldable>().Drop();
        }
    }

    private void SetStunEnemy(bool value)
    {
        EnemyMovementController.SetMovement(!value);
        EnemyAttackController.CanAttack = !value;
    }

    public IEnumerator StunEnemyWithSpecificTime(float seconds)
    {
        IsStunned= true;
        Debug.Log("stunned");
        yield return new WaitForSeconds(seconds);
        IsStunned = false;
        Debug.Log("not stunned");

        Debug.Log(EnemyMovementController.CanMove);
    }

}
