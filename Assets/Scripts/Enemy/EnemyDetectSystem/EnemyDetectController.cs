using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectController : MonoBehaviour
{
    [field: SerializeField] private Enemy enemy;

    private IEnemyDetectService _enemyDetectService;

    private void Awake()
    {
        _enemyDetectService = InGameIoC.Instance.EnemyDetectService;
    }

    private void Update()
    {
        HandleEnemyCameraDetect();
    }

    private void HandleEnemyCameraDetect()
    {
        if (_enemyDetectService.IsVisibleInCamera(Camera.main, enemy)&&!enemy.EnemyHealth.IsDead)
        {
            if (!Player.Instance.PlayerAttackController.TargetEnemyList.Contains(enemy))
            {
                Player.Instance.PlayerAttackController.TargetEnemyList.Add(enemy);
                Debug.Log("VISIBLE");
            }
        }
        else
        {
            if (Player.Instance.PlayerAttackController.TargetEnemyList.Contains(enemy))
            {
                Player.Instance.PlayerAttackController.TargetEnemyList.Remove(enemy);
            }
        }
    }
}
