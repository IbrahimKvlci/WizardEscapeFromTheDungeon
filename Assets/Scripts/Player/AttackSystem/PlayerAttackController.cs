using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private MagicBase justAttackedMagic;


    [field:Header("AttackSettings")]
    [field: SerializeField] public float Damage;
    [field: SerializeField] public float AttackTimerMax;
    [field: SerializeField] public float AttackFreezeTimerMax;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask enemyLayer;
    public bool CanAttack { get; set; }
    public bool IsAttacking { get; set; }

    [field:Header("Index")]
    private int targetEnemyIndex;
    private int magicIndex;

    [field: Header("Counters")]
    private float stunMagicTimer;



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
        CanAttack = true;
        stunMagicTimer = 0;
    }

    private void _inputService_OnSwitchMagicPressed(object sender, IInputService.OnSwitchMagicPressedEventArgs e)
    {
        magicIndex = e.magicIndex;
        Magic = magicList[magicIndex];

    }

    private void Update()
    {
        if (CanAttack&&player.HasWand)
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
        }

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
        #region TriggerEnemies
        TargetEnemyList.Clear();
        List<RaycastHit> hitList = Physics.BoxCastAll(Camera.main.transform.position, new Vector3(3, 3, 3), Camera.main.transform.forward, Camera.main.transform.rotation, maxDistance, enemyLayer).ToList<RaycastHit>();
        if(hitList.Count > 0 )
        {
            Debug.Log("hit");
            foreach (RaycastHit hitObject in hitList)
            {
                if (hitObject.transform.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    if(!enemy.EnemyHealth.IsDead)
                        TargetEnemyList.Add(enemy);
                }
                Debug.Log(hitObject.transform.name);
            }
        }
        #endregion

        if (justAttackedMagic != null)
        {
            stunMagicTimer += Time.deltaTime;
            if (stunMagicTimer >= 2)
            {
                stunMagicTimer = 0;
                justAttackedMagic= null;
            }
        }
    }

    public IEnumerator Attack(Enemy enemy)
    {
        IsAttacking = true;
        OnAttack?.Invoke(this, EventArgs.Empty);
        HandleEnemyStunMagic(enemy);
        player.PlayerMovementController.CanMove = false;

        MagicBase magicBase = Instantiate(Magic.MagicSO.prefab, magicFireLoc.transform.position, Quaternion.identity).GetComponent<MagicBase>();
        magicBase.TargetObject = enemy.gameObject;
        magicBase.MagicTimerMax = AttackTimerMax;
        justAttackedMagic = Magic;
        magicFreezeTimerList[magicIndex] = 0;
        yield return new WaitForSeconds(AttackTimerMax);
        enemy.EnemyHealth.TakeDamage(Damage);
        player.PlayerMovementController.CanMove = true;
        Debug.Log("Attacked" + enemy.name);
        IsAttacking = false;
    }

    private void HandleEnemyStunMagic(Enemy enemy)
    {
        if(justAttackedMagic is FireMagic && Magic is IceMagic)
        {
            Debug.Log("Stun");
            enemy.StartCoroutine(enemy.StunEnemyWithSpecificTime(10));
        }
    }
}
