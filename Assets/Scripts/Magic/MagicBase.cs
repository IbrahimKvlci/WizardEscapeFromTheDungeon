using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBase : MonoBehaviour
{
    [field:SerializeField] public MagicSO MagicSO {  get; private set; }

    public Enemy TargetEnemy { get; set; }
    public float MagicTimerMax { get; set; }

    private Vector3 startingPos;
    private float timer;

    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        if (TargetEnemy != null)
        {
            timer += Time.deltaTime;
            float transformPercantage=timer/MagicTimerMax;

            transform.position = Vector3.Slerp(startingPos, TargetEnemy.transform.position, transformPercantage);
        }
    }
}
