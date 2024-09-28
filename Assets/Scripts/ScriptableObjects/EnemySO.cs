using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    public float health;
    public float enemySpeed;
    public float enemyAttackRange;
    public float enemyAimAttackTimerMax;
    public float enemyAttackTimerMax;
}
