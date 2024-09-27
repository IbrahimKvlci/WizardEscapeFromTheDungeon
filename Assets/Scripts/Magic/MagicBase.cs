using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBase : MonoBehaviour
{
    [field:SerializeField] public MagicSO MagicSO {  get; private set; }

    public GameObject TargetObject { get; set; }
    public float MagicTimerMax { get; set; }

    private Vector3 startingPos;
    private float timer;

    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        if (TargetObject != null)
        {
            timer += Time.deltaTime;
            float transformPercantage=timer/MagicTimerMax;

            transform.position = Vector3.Slerp(startingPos, TargetObject.transform.position, transformPercantage);
            if (transformPercantage >= 1f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        Instantiate(MagicSO.HitParticle, transform.position, Quaternion.identity);
    }
}
