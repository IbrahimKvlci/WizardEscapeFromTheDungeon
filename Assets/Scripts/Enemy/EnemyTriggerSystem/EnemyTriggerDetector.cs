using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerDetector : MonoBehaviour
{
    public bool IsEnemyTriggedToBeAttacked { get; set; }

    private void Start()
    {
        IsEnemyTriggedToBeAttacked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        IsEnemyTriggedToBeAttacked = true;
    }
    private void OnTriggerExit(Collider other)
    {
        IsEnemyTriggedToBeAttacked = false;
    }
}
