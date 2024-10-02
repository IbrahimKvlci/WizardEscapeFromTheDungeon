using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemBoss : Enemy
{
    [SerializeField] public GameObject rockPrefab;
    [SerializeField] public Transform rockLocation;
    [SerializeField] public GameObject earthquakeRockPrefab;
    [SerializeField] public GameObject earthquakeRockMarkerPrefab;

    [field:SerializeField] public RockGolemBossVisual RockGolemBossVisual {  get; set; }

    public float EarthquakeTimer {  get; set; }
    public float ThrowRockTimer { get; set; }
    public bool CanAttack { get; set; }

    public IRockGolemBossEnemyState IdleState {  get; set; }
    public IRockGolemBossEnemyState ChaseState { get; set; }
    public IRockGolemBossEnemyState PunchState { get; set; }
    public IRockGolemBossEnemyState ThrowRockState { get; set; }
    public IRockGolemBossEnemyState EarthquakeState { get; set; }


    private IRockGolemBossEnemyStateService _rockGolemBossEnemyStateService;

    protected override void Awake()
    {
        base.Awake();
        _rockGolemBossEnemyStateService = new RockGolemBossEnemyStateManager();

        IdleState=new RockGolemBossEnemyIdleState(this,_rockGolemBossEnemyStateService);
        ChaseState = new RockGolemBossEnemyChaseState(this, _rockGolemBossEnemyStateService);
        PunchState = new RockGolemBossEnemyPunchState(this, _rockGolemBossEnemyStateService);
        ThrowRockState = new RockGolemBossEnemyThrowRockState(this, _rockGolemBossEnemyStateService);
        EarthquakeState = new RockGolemBossEnemyEarthquakeState(this, _rockGolemBossEnemyStateService);

    }

    protected override void Start()
    {
        base.Start();
        CanAttack = false;
        _rockGolemBossEnemyStateService.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        _rockGolemBossEnemyStateService.CurrentState.UpdateState();

        EarthquakeTimer += Time.deltaTime;
        ThrowRockTimer += Time.deltaTime;
    }
}
