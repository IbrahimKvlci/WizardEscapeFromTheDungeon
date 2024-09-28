using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBossEnemyEarthquakeState : RockGolemBossEnemyStateBase
{
    private float timer;

    public RockGolemBossEnemyEarthquakeState(RockGolemBoss rockGolemBoss, IRockGolemBossEnemyStateService rockGolemBossEnemyStateService) : base(rockGolemBoss, rockGolemBossEnemyStateService)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        timer = 0;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (timer >= 4)
        {
            _rockGolemBoss.StartCoroutine(CreateRock());
            timer = 0;
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
