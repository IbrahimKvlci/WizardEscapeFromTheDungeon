using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitchEnemy : BasicEnemy
{
    [SerializeField] private GameObject litchEnemyAttackMagicPrefab;
    [SerializeField] private Transform magicPos;

    public override void Attack(out bool isAttacking)
    {
        LitchEnemyAttackMagic litchEnemyAttackMagic = Instantiate(litchEnemyAttackMagicPrefab,magicPos.transform.position,Quaternion.identity).GetComponent<LitchEnemyAttackMagic>();
        litchEnemyAttackMagic.Damage = EnemySO.enemyDamage;
        litchEnemyAttackMagic.GiveVelocity((Player.Instance.EnemyTargetTransform.position-litchEnemyAttackMagic.transform.position).normalized);

        isAttacking = false;
    }

    protected override void Start()
    {
        base.Start();
        IdleEnemyAction = () => { };
        EnemyAttackController.CanAttack = true;
    }
}
