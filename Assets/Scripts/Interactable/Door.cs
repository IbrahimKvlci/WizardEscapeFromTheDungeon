using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Quaternion newRotation;
    [SerializeField] private float doorOpenTimerMax;

    private Quaternion oldRotation;
    private bool isDoorOpening;
    private float timer;

    private void Update()
    {
        if (isDoorOpening)
        {
            timer += Time.deltaTime;
            float percantage = timer / doorOpenTimerMax;

            transform.rotation = Quaternion.Lerp(oldRotation, newRotation,percantage);
            if (percantage > 1)
            {
                isDoorOpening = false;
            }
        }
    }

    public void Interact()
    {
        if (Player.Instance.PlayerCollectItemController.KeyCount > 0)
        {
            oldRotation = transform.rotation;
            isDoorOpening=true;
            Player.Instance.PlayerCollectItemController.KeyCount--;
        }
    }
}
