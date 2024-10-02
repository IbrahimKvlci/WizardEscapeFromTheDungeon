using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public event EventHandler OnDoorIsOpened;

    [SerializeField] private Quaternion newRotation;
    [SerializeField] private float doorOpenTimerMax;

    [SerializeField] private bool isDoorLocked;

    private Quaternion oldRotation;
    private bool isDoorOpening;
    private bool isDoorOpen;
    private float timer;

    private void Start()
    {
        isDoorOpen = false;
    }

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
                isDoorOpen = true;

                OnDoorIsOpened?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void OpenDoor()
    {
        oldRotation = transform.rotation;
        isDoorOpening=true;
    }

    public void Interact()
    {
        if (isDoorLocked)
        {
            if (Player.Instance.PlayerCollectItemController.KeyCount > 0 && !isDoorOpen)
            {
                OpenDoor();
                Player.Instance.PlayerCollectItemController.KeyCount--;
                isDoorLocked=false;
            }
        }
        else
        {
            OpenDoor();
        }
        
    }
}
