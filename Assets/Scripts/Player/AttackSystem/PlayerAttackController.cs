using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public event EventHandler OnAttack;

    [field:Header("References")]
    private IInputService _inputService;
    [SerializeField] private Player player;
    [field: SerializeField] private List<MagicBase> magicList;
    private List<float> magicFreezeTimerList;
    public List<Enemy> TargetEnemyList { get; set; }
    [SerializeField] private Transform magicFireLoc;
    public Enemy TargetEnemy { get; set; }
    public MagicBase Magic { get; set; }


    [field:Header("AttackSettings")]
    [field: SerializeField] public float Damage;
    [field: SerializeField] public float AttackTimerMax;
    [field: SerializeField] public float AttackFreezeTimerMax;

    [field:Header("Index")]
    private int targetEnemyIndex;
    private int magicIndex;



    private void Awake()
    {
        _inputService = InGameIoC.Instance.InputService;
        TargetEnemyList = new List<Enemy>();
        magicFreezeTimerList = new List<float>();

        #region CreateFreezeTimers
        for (int i = 0; i < magicList.Count; i++)
        {
            magicFreezeTimerList.Add(magicList[i].MagicSO.freezeTimerMax);
        }
        #endregion
    }

    private void Start()
    {
        _inputService.OnSwitchMagicPressed += _inputService_OnSwitchMagicPressed;

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
        if (magicFreezeTimerList[magicIndex] >= Magic.MagicSO.freezeTimerMax)
        {
            if (_inputService.FireButtonPressed())
            {
                if (TargetEnemyList.Count > 0)
                {
                    StartCoroutine(Attack(TargetEnemy));
                }
            }
        }
        for (int i = 0; i < magicFreezeTimerList.Count; i++)
        {
            if (magicFreezeTimerList[i] <= magicList[i].MagicSO.freezeTimerMax)
            {
                magicFreezeTimerList[i] += Time.deltaTime;
            }
        }
        #endregion

        #region SetTargetEnemy
        if (TargetEnemyList.Count == 0)
        {
            targetEnemyIndex = 0;
            TargetEnemy = null;
        }
        else
        {
            TargetEnemy = TargetEnemyList[targetEnemyIndex];
        }
        #endregion
    }

    public IEnumerator Attack(Enemy enemy)
    {

        OnAttack?.Invoke(this, EventArgs.Empty);

        player.PlayerMovementController.CanMove = false;

        MagicBase magicBase = Instantiate(Magic.MagicSO.prefab, magicFireLoc.transform.position, Quaternion.identity).GetComponent<MagicBase>();
        magicBase.TargetEnemy = enemy;
        magicBase.MagicTimerMax = AttackTimerMax;

        magicFreezeTimerList[magicIndex] = 0;
        yield return new WaitForSeconds(AttackTimerMax);
        enemy.EnemyHealth.TakeDamage(Damage);
        player.PlayerMovementController.CanMove = true;
        Debug.Log("Attacked" + enemy.name);

    }
}
