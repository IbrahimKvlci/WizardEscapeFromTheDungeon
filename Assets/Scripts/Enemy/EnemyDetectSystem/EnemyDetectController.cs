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
        if (Player.Instance.PlayerAttackController.TargetEnemy == null)
        {
            if (_enemyDetectService.IsVisibleInCamera(Camera.main, enemy))
            {
                Player.Instance.PlayerAttackController.TargetEnemy= enemy;
                Debug.Log("VISIBLE");
            }
        }
        else if(Player.Instance.PlayerAttackController.TargetEnemy == enemy)
        {
            if (!_enemyDetectService.IsVisibleInCamera(Camera.main, enemy))
            {
                Player.Instance.PlayerAttackController.TargetEnemy = null;
            }
        }
    }
}
