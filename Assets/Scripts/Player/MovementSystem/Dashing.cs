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
                StartCoroutine(Dash());
                dashTimer = 0;
            }
        }
        else
        {
            dashTimer += Time.deltaTime;
        }
        
    }

    private IEnumerator Dash()
    {
        IsDashing = true;
        player.IsStunned = true;
        Vector3 forceToApply=orientation.forward*dashForce+orientation.up*dashUpwardForce;
        rb.AddForce(forceToApply,ForceMode.Impulse);

        yield return new WaitForSeconds(dashDuration);
        ResetDash();
        yield return new WaitForSeconds(1);
        player.IsStunned = false;
    }

    private void ResetDash()
    {
        rb.velocity = Vector3.zero;
        IsDashing=false;

    }
}
