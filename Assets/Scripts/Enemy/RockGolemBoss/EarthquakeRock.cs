using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeRock : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float timerMax;
    [SerializeField] private float destroyTimerMax;

    private float timer;
    private Vector3 firstPos;


    private void Start()
    {
        timer = 0;
        firstPos = transform.position;
    }

    private void Update()
    {
        float percantage = timer / timerMax;
        transform.position = Vector3.Slerp(firstPos, firstPos + Vector3.up * 2.5f, percantage);

        if (timer>=destroyTimerMax)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            //Damage
            player.PlayerHealth.TakeDamage(damage);
            player.StartCoroutine(player.StunPlayerWithSpecificTime(3));
        }
    }
}
