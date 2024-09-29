using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyEarthquakeState : RockGolemBossEnemyStateBase
{
    public event EventHandler OnEarthquake;

    private float timer;

    public RockGolemBossEnemyEarthquakeState(RockGolemBoss rockGolemBoss, IRockGolemBossEnemyStateService rockGolemBossEnemyStateService) : base(rockGolemBoss, rockGolemBossEnemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        OnEarthquake?.Invoke(this, EventArgs.Empty);
        timer = 0;
        _rockGolemBoss.EarthquakeTimer = 0;

        CanChangeState = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (timer >= 8)
        {
            _rockGolemBoss.StartCoroutine(CreateRock());
            timer = 0;
            CanChangeState = true;

            _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.IdleState);
        }
        else 
        { 
            timer += Time.deltaTime; 
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private IEnumerator CreateRock()
    {
        Vector3 playerPos = Player.Instance.transform.position;
        GameObject marker= GameObject.Instantiate(_rockGolemBoss.earthquakeRockMarkerPrefab, playerPos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        GameObject.Destroy(marker);
        GameObject earthquakeRock = GameObject.Instantiate(_rockGolemBoss.earthquakeRockPrefab, playerPos, Quaternion.identity);

    }
}
