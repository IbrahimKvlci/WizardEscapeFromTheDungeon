using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    public float Health { get; set; }
    public bool IsDead { get; set; }

    private void Start()
    {
        Health=enemy.EnemySO.health;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            IsDead = true;
        }
    }

    public void DestroySelf()
    {
        enemy.DropHoldableObjectsOnEnemy();
        Destroy(enemy.gameObject);
    }
}
