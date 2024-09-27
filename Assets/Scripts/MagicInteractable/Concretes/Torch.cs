using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IMagicInteractable
{
    [SerializeField] private GameObject lightObject;

    public bool isTorchLighted { get; set; }
    public GameObject MagicInteractableObject { get ; set ; }

    private void Start()
    {
        isTorchLighted = lightObject.activeSelf;
        MagicInteractableObject = gameObject;
    }

    public void Interact(MagicBase magic)
    {
        if(magic is IceMagic)
            ExtinguishTheTorch();
        else if(magic is FireMagic)
            LightTheTorch();
    }

    private void LightTheTorch()
    {
        lightObject.SetActive(true);
        isTorchLighted = true;
    }
    private void ExtinguishTheTorch()
    {
        lightObject.SetActive(false);
        isTorchLighted = false;
    }
}
