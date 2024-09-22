using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPassDoor : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("New Level");
    }
}
