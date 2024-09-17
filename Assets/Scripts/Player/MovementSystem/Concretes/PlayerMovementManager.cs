using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : IPlayerMovementService
{
    private readonly IInputService _inputService;

    

    public PlayerMovementManager(IInputService inputService)
    {
        _inputService = inputService;
    }

    public void Jump(Player player)
    {
        player.Rigidbody.velocity = new Vector3(player.Rigidbody.velocity.x, player.PlayerMovementController.JumpSpeed, player.Rigidbody.velocity.z);
    }

    public void HandleMovement(Player player)
    {
        float horizontal = _inputService.GetMovementVector().x;
        float vertical = _inputService.GetMovementVector().y;

        Vector3 moveVector = new Vector3(horizontal, 0, vertical);
        moveVector.Normalize();

        if (moveVector.x > 0 || moveVector.x < 0)
        {
            if (!Physics.Raycast(player.transform.position + Vector3.up * 1, Mathf.Sign(moveVector.x)* Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized, 0.5f))
            {
                player.transform.Translate(moveVector * Time.deltaTime * player.PlayerMovementController.Speed);
            }
            else
            {
                moveVector.x = 0;
            }
        }
        else if(moveVector.z>0||moveVector.z < 0)
        {
            if (!Physics.Raycast(player.transform.position + Vector3.up * 1, Mathf.Sign(moveVector.z) * Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized, 0.5f))
            {
                player.transform.Translate(moveVector * Time.deltaTime * player.PlayerMovementController.Speed);
            }
            else
            {
                moveVector.z = 0;
            }
        }

        player.PlayerMovementController.IsRunning = moveVector != Vector3.zero;
        player.PlayerMovementController.IsMoving = moveVector != Vector3.zero;
        
    }

}
