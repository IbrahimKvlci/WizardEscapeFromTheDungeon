using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectController : MonoBehaviour
{
    [field: SerializeField] private Enemy enemy;

    private IObjectDetectService _objectDetectService;

    private void Awake()
    {
        _objectDetectService = InGameIoC.Instance.ObjectDetectService;
    }

    private void Update()
    {
        HandleEnemyCameraDetect();
    }

    private void HandleEnemyCameraDetect()
    {
        if (_objectDetectService.IsVisibleInCamera(Camera.main, enemy.gameObject)&&!enemy.EnemyHealth.IsDead)
        {
            if (!Player.Instance.PlayerAttackController.TargetEnemyList.Contains(enemy))
            {
                Player.Instance.PlayerAttackController.TargetEnemyList.Add(enemy);
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
