using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    #region Info
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float attackCoolDown = 0.5f;

    private PlayerControls playerControls;
    private Animator anim;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private GameObject slashAnim;

    private bool attackButtonDown, isAttacking = false;
    #endregion

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        anim = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }
    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }
    private void OnEnable() => playerControls.Enable();
    #region Attack Logic
    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown = false;
    }
    private void Attack()
    {
        if(attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDCoroutine());
        }
    }
    private IEnumerator AttackCDCoroutine()
    {
        yield return new WaitForSeconds(attackCoolDown);
        isAttacking = false;
    }
    #endregion
    public void DontAttackingEvent() => weaponCollider.gameObject.SetActive(false);
    #region Flip
    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if(playerController.FacingLeft)
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
    }
    #endregion
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        //float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
