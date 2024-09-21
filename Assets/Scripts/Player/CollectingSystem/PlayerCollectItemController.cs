using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectItemController : MonoBehaviour
{
    public int KeyCount { get; set; }

    private void Start()
    {
        KeyCount = 0;
    }

    public void CollectKey()
    {
        KeyCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollectable>(out ICollectable collectable))
        {
            collectable.Collect();
        }
    }
}
