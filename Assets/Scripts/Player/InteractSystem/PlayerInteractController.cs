using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    [SerializeField] private LayerMask layer;

    private void Update()
    {
        if (Physics.Raycast(Player.Instance.transform.position, Player.Instance.transform.forward, out RaycastHit hitInfo,5f,layer))
        {
            if (hitInfo.transform.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
    }
}
