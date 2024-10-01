using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyEarthquakeState : RockGolemBossEnemyStateBase
{
    public event EventHandler OnEarthquake;


    public RockGolemBossEnemyEarthquakeState(RockGolemBoss rockGolemBoss, IRockGolemBossEnemyStateService rockGolemBossEnemyStateService) : base(rockGolemBoss, rockGolemBossEnemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _rockGolemBoss.RockGolemBossVisual.OnEarthquakeStarted += RockGolemBossVisual_OnEarthquakeStarted;
        _rockGolemBoss.RockGolemBossVisual.OnEarthquakeFinished += RockGolemBossVisual_OnEarthquakeFinished;

        OnEarthquake?.Invoke(this, EventArgs.Empty);
        _rockGolemBoss.EarthquakeTimer = 0;

        CanChangeState = false;
    }

    private void RockGolemBossVisual_OnEarthquakeFinished(object sender, EventArgs e)
    {
        CanChangeState = true;
        _rockGolemBossEnemyStateService.SwitchState(_rockGolemBoss.IdleState);

    }

    private void RockGolemBossVisual_OnEarthquakeStarted(object sender, EventArgs e)
    {
        _rockGolemBoss.StartCoroutine(CreateRock());
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
        _rockGolemBoss.RockGolemBossVisual.OnEarthquakeStarted -= RockGolemBossVisual_OnEarthquakeStarted;
        _rockGolemBoss.RockGolemBossVisual.OnEarthquakeFinished -= RockGolemBossVisual_OnEarthquakeFinished;

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
