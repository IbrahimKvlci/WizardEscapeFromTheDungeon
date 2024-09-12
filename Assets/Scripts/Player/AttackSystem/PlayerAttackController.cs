using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public event EventHandler OnAttack;

    [field: SerializeField] public float Damage;
    [field: SerializeField] public float AttackTimerMax;
    [field: SerializeField] public float AttackFreezeTimerMax;

    [SerializeField] private Transform magicFireLoc;
    [SerializeField] private Player player;

    public List<Enemy> TargetEnemyList { get; set; }
    public Enemy TargetEnemy { get; set; }
    private int targetEnemyIndex;


    [field: SerializeField] private List<MagicBase> magicList;
    public MagicBase Magic { get; set; }
    private int magicIndex; 

    private IInputService _inputService;

    private float attackFreezeTimer;

    private void Awake()
    {
        _inputService=InGameIoC.Instance.InputService;
        TargetEnemyList = new List<Enemy>();
    }

    private void Start()
    {
        _inputService.OnSwitchMagicPressed += _inputService_OnSwitchMagicPressed;

        attackFreezeTimer = 0;
        targetEnemyIndex = 0;
        magicIndex = 0;
        Magic = magicList[magicIndex];
    }

    private void _inputService_OnSwitchMagicPressed(object sender, IInputService.OnSwitchMagicPressedEventArgs e)
    {
        magicIndex = e.magicIndex;
        Magic = magicList[magicIndex];

    }

    private void Update()
    {
        #region AttackTimer
        if ( attackFreezeTimer >= AttackFreezeTimerMax)
        {
            if (_inputService.FireButtonPressed())
            {
                if (TargetEnemyList.Count>0)
                {
                    StartCoroutine(Attack(TargetEnemy));
                }
            }
        }
        else
        {
            attackFreezeTimer += Time.deltaTime;
        }
        #endregion

        #region SetTargetEnemy
        if (TargetEnemyList.Count==0)
        {
            targetEnemyIndex = 0;
            TargetEnemy= null;
        }
        else
        {
            TargetEnemy=TargetEnemyList[targetEnemyIndex];
        }
        #endregion
    }

    public IEnumerator Attack(Enemy enemy)
    {

        OnAttack?.Invoke(this, EventArgs.Empty);

        player.PlayerMovementController.CanMove = false;

        MagicBase magicBase = Instantiate(Magic.MagicSO.prefab,magicFireLoc.transform.position,Quaternion.identity).GetComponent<MagicBase>();
        magicBase.TargetEnemy = enemy;
        magicBase.MagicTimerMax = AttackTimerMax;

        attackFreezeTimer = 0;
        yield return new WaitForSeconds(AttackTimerMax);
        enemy.EnemyHealth.TakeDamage(Damage);
        player.PlayerMovementController.CanMove = true;
        Debug.Log("Attacked" + enemy.name);

    }
}
