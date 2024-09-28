using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public event EventHandler OnAttackStarted;
    public event EventHandler OnAttackEnded;

    public bool CanAttack { get; set; } = false;
    
    public void AttackStarted()
    {
        OnAttackStarted?.Invoke(this,EventArgs.Empty);
    }
    public void AttackFinished()
    {
        OnAttackEnded?.Invoke(this,EventArgs.Empty);
    }
}
