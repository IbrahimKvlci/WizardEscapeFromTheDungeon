using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [field:SerializeField] public float Health { get; set; }
    public bool IsPlayerAlive { get; set; }

    private void Start()
    {
        IsPlayerAlive = true;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            DestroyPlayer();
        }
    }

    private void DestroyPlayer()
    {
        Debug.Log("Player is dead");
        IsPlayerAlive = false;
    }
}
