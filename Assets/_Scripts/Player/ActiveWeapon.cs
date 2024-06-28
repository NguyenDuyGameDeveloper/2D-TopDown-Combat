using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    private PlayerControls playerControls;

    private float timeBetweenAttack;
    private bool attackButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown();
    }
    private void Update()
    {
        Attack();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttackCoroutine());
    }
    private IEnumerator TimeBetweenAttackCoroutine()
    {
        yield return new WaitForSeconds(timeBetweenAttack);
        isAttacking = false;
    }
    public void WeaponNull() => CurrentActiveWeapon = null;
    public void NewWeapon(MonoBehaviour _newWeapon)
    {
        CurrentActiveWeapon = _newWeapon;
        AttackCooldown();
        timeBetweenAttack = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }
    private void StartAttacking() => attackButtonDown = true;
    private void StopAttacking() => attackButtonDown = false;
    private void Attack()
    {
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}
