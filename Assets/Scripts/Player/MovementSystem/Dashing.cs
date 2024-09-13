using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [field: Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Player player;
    private Rigidbody rb;
    private IInputService _inputService;

    [field: Header("Dashing Settings")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashUpwardForce;
    [SerializeField] private float dashDuration;

    [field: Header("Cooldown")]
    [SerializeField] private float dashTimerMax;
    private float dashTimer;

    [field: Header("Porperties")]
    public bool IsDashing { get; set; }


    private void Awake()
    {
        _inputService=InGameIoC.Instance.InputService;
        rb = player.Rigidbody;
    }

    private void Start()
    {
        dashTimer = dashTimerMax;
        IsDashing = false;  
    }

    private void Update()
    {
        if (dashTimer >= dashTimerMax)
        {
            if (_inputService.DashButtonPressed() && player.PlayerMovementController.CanMove)
            {
                Dash();
                dashTimer = 0;
            }
        }
        else
        {
            dashTimer += Time.deltaTime;
        }
        
    }

    private void Dash()
    {
        IsDashing = true;
        Vector3 forceToApply=orientation.forward*dashForce+orientation.up*dashUpwardForce;
        rb.AddForce(forceToApply,ForceMode.Impulse);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {
        rb.velocity = Vector3.zero;
        IsDashing=false;
    }
}
