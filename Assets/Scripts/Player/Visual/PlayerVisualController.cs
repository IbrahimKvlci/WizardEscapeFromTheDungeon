using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;

    enum AnimationEnum
    {
        IsWalking,
        JumpStartTrigger,
        IsDowning,
        AttackTrigger,
        IsStunned,
        IdleBlend,
        IsHolding,
    }

    private void Start()
    {
        player.PlayerMovementController.OnRunningChanged += PlayerMovementController_OnRunningChanged;
        player.PlayerMovementController.OnGroundedChanged += PlayerMovementController_OnGroundedChanged;
        player.PlayerMovementController.OnJump += PlayerMovementController_OnJump;
        player.PlayerMovementController.OnFallingChanged += PlayerMovementController_OnFallingChanged;
        player.PlayerAttackController.OnAttack += PlayerAttackController_OnAttack;
        player.OnStunChanged += Player_OnStunChanged;
        player.OnWandActiveChanged += Player_OnWandActiveChanged;
        player.HoldingObjectController.OnHoldingChanged += HoldingObjectController_OnHoldingChanged;
    }

    private void HoldingObjectController_OnHoldingChanged(object sender, System.EventArgs e)
    {
        SetAnimationBool(AnimationEnum.IsHolding, player.HoldingObjectController.IsHolding);
    }


    private void Player_OnWandActiveChanged(object sender, System.EventArgs e)
    {
        if (player.HasWand)
        {
            SetAnimationFloat(AnimationEnum.IdleBlend, 1);
        }
        else
        {
            SetAnimationFloat(AnimationEnum.IdleBlend, 0);
        }
    }

    private void Player_OnStunChanged(object sender, System.EventArgs e)
    {
        SetAnimationBool(AnimationEnum.IsStunned, player.IsStunned);
    }

    private void PlayerAttackController_OnAttack(object sender, System.EventArgs e)
    {
        TriggerAnimationById(AnimationEnum.AttackTrigger,Random.Range(1, 3));
    }

    private void PlayerMovementController_OnFallingChanged(object sender, System.EventArgs e)
    {
        SetAnimationBool(AnimationEnum.IsDowning, player.PlayerMovementController.IsFalling);
    }

    private void PlayerMovementController_OnGroundedChanged(object sender, System.EventArgs e)
    {
        //if(player.PlayerMovementController.Grounded)
        //    SetAnimationBool(AnimationEnum.IsDowning,false);
    }

    private void PlayerMovementController_OnJump(object sender, System.EventArgs e)
    {
        TriggerAnimation(AnimationEnum.JumpStartTrigger);
    }

    private void OnDisable()
    {
        player.PlayerMovementController.OnRunningChanged -= PlayerMovementController_OnRunningChanged;
        player.PlayerMovementController.OnGroundedChanged -= PlayerMovementController_OnGroundedChanged;
        player.PlayerMovementController.OnJump -= PlayerMovementController_OnJump;
        player.PlayerMovementController.OnFallingChanged -= PlayerMovementController_OnFallingChanged;
        player.OnStunChanged -= Player_OnStunChanged;
        player.OnWandActiveChanged -= Player_OnWandActiveChanged;
        player.HoldingObjectController.OnHoldingChanged -= HoldingObjectController_OnHoldingChanged;
    }

    private void PlayerMovementController_OnRunningChanged(object sender, System.EventArgs e)
    {
        SetAnimationBool(AnimationEnum.IsWalking, player.PlayerMovementController.IsRunning);
    }

    private void SetAnimationBool(AnimationEnum animationEnum, bool value)
    {
        animator.SetBool(animationEnum.ToString(), value);
    }
    private void TriggerAnimation(AnimationEnum animationEnum)
    {
        animator.SetTrigger(animationEnum.ToString());
    }
    private void TriggerAnimationById(AnimationEnum animationEnum,int id)
    {
        animator.SetTrigger($"{animationEnum.ToString()}{id}");
    }
    private void SetAnimationFloat(AnimationEnum animationEnum,float value)
    {
        animator.SetFloat(animationEnum.ToString(), value);
    }
}
