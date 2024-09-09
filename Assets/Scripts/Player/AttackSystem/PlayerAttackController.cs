using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [field: SerializeField] public float Damage;
    [field: SerializeField] public float AttackTimerMax;
    [field: SerializeField] public float AttackFreezeTimerMax;

    public Enemy TargetEnemy { get; set; }

    private Enemy WillBeAttackedEnemy;

    private IInputService _inputService;

    private void Awake()
    {
        _inputService=InGameIoC.Instance.InputService;
    }

    private void Update()
    {
        if (_inputService.FireButtonPressed())
        {
            if(TargetEnemy != null)
            {
                WillBeAttackedEnemy = TargetEnemy;
            }
        }
    }

    public void Attack(Enemy enemy)
    {
        enemy.EnemyHealth.TakeDamage(Damage);
        Debug.Log("Attacked" + enemy.name);
    }
}
