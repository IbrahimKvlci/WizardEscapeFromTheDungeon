using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicInteractController : MonoBehaviour
{
    public event EventHandler OnMagicInteract;

    [SerializeField] private LayerMask layer;
    [SerializeField] private float rayLength;


    private void Update()
    {
        if (Player.Instance.PlayerAttackController.TargetEnemy == null)
        {
            if (Physics.BoxCast(Camera.main.transform.position, new Vector3(1, 1, 1), Camera.main.transform.forward, out RaycastHit hitInfo, Camera.main.transform.rotation, rayLength, layer))
            {
                if (hitInfo.transform.TryGetComponent<IMagicInteractable>(out IMagicInteractable interactable))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        OnMagicInteract?.Invoke(this,EventArgs.Empty);
                        StartCoroutine(Interact(interactable));
                        Debug.Log("Interacted");
                    }
                }
            }
        }
    }

    private IEnumerator Interact(IMagicInteractable interactable)
    {
        MagicBase magicBase=Instantiate(Player.Instance.PlayerAttackController.Magic);
        magicBase.transform.position=Player.Instance.MagicLocation.transform.position;
        magicBase.TargetObject = interactable.MagicInteractableObject;
        magicBase.MagicTimerMax = 0.5f;
        yield return new WaitForSeconds(0.5f);
        interactable.Interact(magicBase);
    }
}
