using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private LayerMask layerMask;

    public bool IsPlayerTriggeredToBePreparedForAttack()
    {
        return Physics.CheckSphere(enemy.transform.position, enemy.EnemySO.enemyAttackRange, layerMask);
    }
    public bool IsPlayerTriggeredToBeAttacked()
    {
        return Physics.Raycast(enemy.transform.position,enemy.transform.forward,enemy.EnemySO.enemyAttackRange, layerMask);
    }
}