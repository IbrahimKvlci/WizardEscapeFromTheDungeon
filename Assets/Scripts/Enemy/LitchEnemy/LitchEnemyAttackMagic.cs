using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitchEnemyAttackMagic : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    public float Damage { get; set; }


    public void GiveVelocity(Vector3 dir)
    {
        _rb.velocity = dir*_speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            player.PlayerHealth.TakeDamage(Damage);
        }
    }
}
