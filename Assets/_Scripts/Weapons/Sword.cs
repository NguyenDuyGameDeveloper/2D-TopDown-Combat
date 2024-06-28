using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    #region Info
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private SO_WeaponInfo weaponInfo;

    private Transform weaponCollider;
    private Transform slashAnimSpawnPoint;
    private Animator anim;
    private GameObject slashAnim;

    #endregion
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        this.weaponCollider = PlayerController.Instance.GetWeaponCollider();
        this.slashAnimSpawnPoint = PlayerController.Instance.GetSlashAnimSpawnPoint();
    }
    private void Update()
    {
        MouseFollowWithOffset();
    }
    public SO_WeaponInfo GetWeaponInfo() => weaponInfo;
    public void DontAttackingEvent() => weaponCollider.gameObject.SetActive(false);
    #region Attack Logic
    public void Attack()
    {
        anim.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }
    #endregion
    #region Flip
    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
    }
    #endregion
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
