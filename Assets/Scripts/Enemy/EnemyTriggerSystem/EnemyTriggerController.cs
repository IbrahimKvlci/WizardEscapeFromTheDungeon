using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private LayerMask layerMask;
    [field:SerializeField] public EnemyTriggerDetector EnemyTriggerDetector {  get; private set; }

    public bool IsPlayerTriggeredToBePreparedForAttack()
    {
        return Physics.CheckSphere(enemy.transform.position, enemy.EnemySO.enemyAttackRange-1, layerMask);
    }
    public bool IsPlayerTriggeredToBeAttacked()
    {
        return Physics.Raycast(enemy.transform.position,enemy.transform.forward,enemy.EnemySO.enemyAttackRange, layerMask);
    }
}
