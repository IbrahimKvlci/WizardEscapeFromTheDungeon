using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health { get; set; }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Debug.Log("Dead");
    }
}
