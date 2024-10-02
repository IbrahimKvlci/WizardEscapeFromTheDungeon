using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyThrowRockState : RockGolemBossEnemyStateBase
{
    public event EventHandler OnThrowingRock;

    private GameObject rockObject;
    private Vector3 firstThrowRockLocation,firstPlayerLocation;

    private float translateTimer;

    private bool isRockThrowed;

    public RockGolemBossEnemyThrowRockState(RockGolemBoss rockGolemBoss, IRockGolemBossEnemyStateService rockGolemBossEnemyStateService) : base(rockGolemBoss, rockGolemBossEnemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _rockGolemBoss.RockGolemBossVisual.OnGetRock += RockGolemBossVisual_OnGetRock;
        _rockGolemBoss.RockGolemBossVisual.OnThrowRock += RockGolemBossVisual_OnThrowRock;
        _rockGolemBoss.RockGolemBossVisual.OnThrowingFinished += RockGolemBossVisual_OnThrowingFinished;

        OnThrowingRock?.Invoke(this,EventArgs.Empty);
        
        isRockThrowed = false;
        translateTimer = 0;
        _rockGolemBoss.ThrowRockTimer= 0;

        CanChangeState = false;

    }

    private void RockGolemBossVisual_OnThrowingFinished(object sender, EventArgs e)
    {

    }

    private void RockGolemBossVisual_OnThrowRock(object sender, EventArgs e)
    {
        firstThrowRockLocation = rockObject.transform.position;
        firstPlayerLocation = Player.Instance.transform.position;
        isRockThrowed= true;
        rockObject.GetComponent<Rock>().CanDamage = true;
    }

    private void RockGolemBossVisual_OnGetRock(object sender, EventArgs e)
    {
        rockObject = GameObject.Instantiate(_rockGolemBoss.rockPrefab, _rockGolemBoss.rockLocation);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (isRockThrowed)
        {
            translateTimer += Time.deltaTime;
            float percantage = translateTimer / 0.5f;

            rockObject.transform.SetParent(null);
            rockObject.transform.position = Vector3.Slerp(firstThrowRockLocation,firstPlayerLocation,percantage);

            if (percantage >= 1)
            {
                isRockThrowed = false;
                CanChangeState = true;
                rockObject.GetComponent<Rock>().CanDamage = false;
                _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.IdleState);
            }
        }
        else
        {
            //Aim
            Vector3 enemyForwardVector = Player.Instance.transform.position - _rockGolemBoss.transform.position;
            _rockGolemBoss.transform.forward = Vector3.Slerp(_rockGolemBoss.transform.forward, enemyForwardVector, 0.05f);

        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _rockGolemBoss.RockGolemBossVisual.OnGetRock -= RockGolemBossVisual_OnGetRock;
        _rockGolemBoss.RockGolemBossVisual.OnThrowRock -= RockGolemBossVisual_OnThrowRock;
        _rockGolemBoss.RockGolemBossVisual.OnThrowingFinished -= RockGolemBossVisual_OnThrowingFinished;
    }


}
