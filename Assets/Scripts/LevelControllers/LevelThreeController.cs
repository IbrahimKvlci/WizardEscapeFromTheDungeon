using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelThreeController : MonoBehaviour
{
    [SerializeField] private List<Enemy> attackPlayerOnStartEnemyList;

    private void Start()
    {
        foreach (Enemy enemy in attackPlayerOnStartEnemyList)
        {
            enemy.EnemyAttackController.CanAttack = true;
        }
    }

}
