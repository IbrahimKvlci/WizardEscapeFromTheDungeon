using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovementService
{
    void HandleMovement(Player player);
    void Jump(Player player);
    void Dash(Player player, float dashForce, float dashUpwardForce);   
}
