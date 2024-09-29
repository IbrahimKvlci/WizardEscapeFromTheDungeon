using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    public event EventHandler OnEnemyMovementChanged;

    [SerializeField] private Enemy enemy;
    [SerializeField] private NavMeshAgent navMeshAgent;

    public bool CanMove { get; set; }

    public void HandleMovementToThePlayer()
    {
        navMeshAgent.destination = Player.Instance.transform.position;
        navMeshAgent.speed = enemy.EnemySO.enemySpeed;
    }
    public void SetMovement(bool canMove)
    {
        CanMove = canMove;
        navMeshAgent.isStopped = !canMove;
        navMeshAgent.ResetPath();

        if (!canMove)
        {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.speed = 0;
        }

        OnEnemyMovementChanged?.Invoke(this, EventArgs.Empty);
    }
}
