using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyThrowRockState : RockGolemBossEnemyStateBase
{
    private GameObject rockObject;
    private Vector3 firstThrowRockLocation,firstPlayerLocation;

    private float throwRockTimer,translateTimer;

    private bool firstFrame;

    public RockGolemBossEnemyThrowRockState(RockGolemBoss rockGolemBoss, IRockGolemBossEnemyStateService rockGolemBossEnemyStateService) : base(rockGolemBoss, rockGolemBossEnemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        rockObject = GameObject.Instantiate(_rockGolemBoss.rockPrefab,_rockGolemBoss.rockLocation);
        firstFrame = true;
        throwRockTimer = 0;
        translateTimer = 0;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (throwRockTimer >= 4)
        {
            if (firstFrame)
            {
                firstThrowRockLocation= rockObject.transform.position;
                firstPlayerLocation=Player.Instance.transform.position;

                firstFrame = false;
            }

            translateTimer += Time.deltaTime;
            float percantage = translateTimer / 0.5f;

            rockObject.transform.SetParent(null);
            rockObject.transform.position = Vector3.Slerp(firstThrowRockLocation,firstPlayerLocation,percantage);

            if (percantage >= 1)
            {
                _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.ChaseState);
            }
        }
        else
        {
            //Aim
            Vector3 enemyForwardVector = Player.Instance.transform.position - _rockGolemBoss.transform.position;
            _rockGolemBoss.transform.forward = Vector3.Slerp(_rockGolemBoss.transform.forward, enemyForwardVector, 0.05f);

            throwRockTimer += Time.deltaTime;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }


}
