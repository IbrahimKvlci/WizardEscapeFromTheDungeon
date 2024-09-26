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
    [SerializeField] private float dashStunTimerMax;
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
            if (_inputService.DashButtonPressed() && player.PlayerMovementController.CanMove&&!Physics.Raycast(player.transform.position+Vector3.up,orientation.forward,1f))
            {
                StartCoroutine(Dash());
                dashTimer = 0;
            }
        }
        else
        {
            dashTimer += Time.deltaTime;
        }

        if (IsDashing)
        {
            if (Physics.Raycast(player.transform.position + Vector3.up, orientation.forward, 0.5f))
            {
                ResetDash();
                Debug.Log("a");
            }
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
        yield return new WaitForSeconds(dashStunTimerMax);
        player.IsStunned = false;
    }

    private void ResetDash()
    {
        rb.velocity = Vector3.zero;
        IsDashing=false;

    }
}
