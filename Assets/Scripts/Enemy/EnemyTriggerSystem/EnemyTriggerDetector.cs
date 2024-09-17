using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerDetector : MonoBehaviour
{
    private bool _isEnemyTriggeredToBeAttacked;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Enemy enemy;

    private void Start()
    {
        _isEnemyTriggeredToBeAttacked = false;
        boxCollider.enabled = false;
        ((EnemyAttackState)enemy.EnemyAttackState).OnAttackStarted += EnemyTriggerDetector_OnAttackStarted;
        ((EnemyAttackState)enemy.EnemyAttackState).OnAttackFinished += EnemyTriggerDetector_OnAttackFinished;

    }

    private void EnemyTriggerDetector_OnAttackFinished(object sender, System.EventArgs e)
    {
        boxCollider.enabled = false;
    }

    private void EnemyTriggerDetector_OnAttackStarted(object sender, System.EventArgs e)
    {
        boxCollider.enabled = true;
        _isEnemyTriggeredToBeAttacked = false;
    }

    public bool IsEnemyTriggeredToBeAttacked()
    {
        return _isEnemyTriggeredToBeAttacked;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _isEnemyTriggeredToBeAttacked = true;
            boxCollider.enabled = false;

        }
    }
}
