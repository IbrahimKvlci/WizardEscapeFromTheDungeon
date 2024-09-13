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
        player.transform.Translate(moveVector * Time.deltaTime * player.PlayerMovementController.Speed);

        player.PlayerMovementController.IsRunning = moveVector != Vector3.zero;
    }

}
