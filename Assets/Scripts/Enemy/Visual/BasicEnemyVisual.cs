using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyVisual : MonoBehaviour
{
    public event EventHandler OnAttackStarted;
    public event EventHandler OnAttackFinished;
    public event EventHandler OnAnimationFinished;

    #region AttackAnimationEventFunctions
    private void AttackStarted()
    {
        OnAttackStarted?.Invoke(this, EventArgs.Empty);
    }
    private void AttackFinished()
    {
        OnAttackFinished?.Invoke(this, EventArgs.Empty);
    }
    private void AnimationFinished()
    {
        OnAnimationFinished?.Invoke(this, EventArgs.Empty);
    }
    #endregion
}
