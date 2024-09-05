using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovementService
{
    void HandleMovement(Player player);
    void HandleJump(Player player);
}
