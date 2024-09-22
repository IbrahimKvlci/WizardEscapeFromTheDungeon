using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Player.Instance.PlayerCollectItemController.CollectWand();
        Destroy(gameObject);
    }
}
