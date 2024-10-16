using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTriggerController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private LayerMask layerMask;
    [field:SerializeField] public EnemyTriggerDetector EnemyTriggerDetector {  get; private set; }

    public bool IsPlayerTriggeredToBePreparedForAttack()
    {
        if (Physics.Raycast(enemy.transform.position, Player.Instance.transform.position - enemy.transform.position, out RaycastHit hitInfo, enemy.EnemySO.enemyAttackRange, layerMask))
        {
            if (hitInfo.transform.TryGetComponent<Player>(out Player player))
                return true;
        }
        return false;
    }
    public bool IsPlayerTriggeredToBeAttacked()
    {
        return Physics.Raycast(enemy.transform.position,enemy.transform.forward,enemy.EnemySO.enemyAttackRange, layerMask);
    }
    public bool IsPlayerTriggeredToBeChased()
    {
        if(Physics.Raycast(enemy.transform.position, Player.Instance.transform.position - enemy.transform.position,out RaycastHit hitInfo, enemy.EnemySO.enemyChaseRange, layerMask))
        {
            if (hitInfo.transform.TryGetComponent<Player>(out Player player))
                return true;
        }
        return false;
    }
}
