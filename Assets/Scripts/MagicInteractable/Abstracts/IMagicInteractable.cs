using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagicInteractable
{
    void Interact(MagicBase magic);

    public GameObject MagicInteractableObject { get; set; }
}
