using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private SO_WeaponInfo weaponInfo;
    [SerializeField] private GameObject staffMagicLaserPrefab;
    [SerializeField] private Transform staffMagicLaserSpawnPoint;

    private Animator anim;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        MouseFollowWithOffset();
    }
    public SO_WeaponInfo GetWeaponInfo() => weaponInfo;
    public void Attack()
    {
        anim.SetTrigger(ATTACK_HASH);
    }
    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject newLaser = Instantiate(staffMagicLaserPrefab,
            staffMagicLaserSpawnPoint.transform.position,Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        else
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
