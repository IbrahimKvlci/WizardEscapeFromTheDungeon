using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [field:SerializeField] public float Speed {  get; set; }
    [field: SerializeField] public float JumpSpeed { get; set; }

    public bool Grounded { get; set; }
    public bool IsRunning { get; set; }

    [SerializeField] private Player player;

    private IPlayerMovementService _playerMovementService;

    private void Awake()
    {
        _playerMovementService=InGameIoC.Instance.PlayerMovementService;
    }

    private void Start()
    {
        Grounded = true;
        IsRunning = false;
    }

    private void Update()
    {
        _playerMovementService.HandleJump(player);
        _playerMovementService.HandleMovement(player);

        Grounded = player.Rigidbody.velocity.y == 0;

    }


}
