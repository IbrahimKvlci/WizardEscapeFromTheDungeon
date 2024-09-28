using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerDetector : MonoBehaviour
{
    private bool _isEnemyTriggeredToBeAttacked;
    [SerializeField] private Collider _collider;
    [SerializeField] private Enemy enemy;

    private void Start()
    {
        _isEnemyTriggeredToBeAttacked = false;
        _collider.enabled = false;
        enemy.EnemyAttackController.OnAttackStarted += EnemyTriggerDetector_OnAttackStarted;
        enemy.EnemyAttackController.OnAttackEnded += EnemyTriggerDetector_OnAttackFinished;

    }

    private void EnemyTriggerDetector_OnAttackFinished(object sender, System.EventArgs e)
    {
        _collider.enabled = false;
    }

    private void EnemyTriggerDetector_OnAttackStarted(object sender, System.EventArgs e)
    {
        _collider.enabled = true;
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
            _collider.enabled = false;

        }
    }
}
