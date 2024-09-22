using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneController : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private Door door;

    private void Start()
    {
        door.OnDoorIsOpened += Door_OnDoorIsOpened;
    }

    private void Door_OnDoorIsOpened(object sender, System.EventArgs e)
    {
        MakeEnemiesAttackPlayer();
    }

    private void MakeEnemiesAttackPlayer()
    {
        foreach (Enemy enemy in enemyList)
        {
            enemy.EnemyAttackController.CanAttack = true;
        }
    }
}
