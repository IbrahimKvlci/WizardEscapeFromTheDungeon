using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public event EventHandler OnAttackStarted;
    public event EventHandler OnAttackEnded;

    [field:SerializeField] public bool CanAttack { get; set; } = false;
    [SerializeField] private bool setCanAttackWhenTriggeredPlayer;
    [SerializeField] private EnemyTriggerController enemyTriggerController;
   
    

    private void Update()
    {
        if (!CanAttack&&setCanAttackWhenTriggeredPlayer&&enemyTriggerController.IsPlayerTriggeredToBeChased())
        {
            CanAttack = true;
        }
    }

    public void AttackStarted()
    {
        OnAttackStarted?.Invoke(this,EventArgs.Empty);
    }
    public void AttackFinished()
    {
        OnAttackEnded?.Invoke(this,EventArgs.Empty);
    }
}
