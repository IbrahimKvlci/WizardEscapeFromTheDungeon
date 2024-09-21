using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Player.Instance.PlayerCollectItemController.CollectKey();
        Destroy(gameObject);
    }
}
