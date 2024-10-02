using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float damage;

    public bool CanDamage { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if(CanDamage)
                player.PlayerHealth.TakeDamage(damage);
        }
    }
}
