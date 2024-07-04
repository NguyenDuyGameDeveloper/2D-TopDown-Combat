using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private SO_WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    private Animator anim;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Attack()
    {
        anim.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.transform.position,
            ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
    }
    public SO_WeaponInfo GetWeaponInfo() => weaponInfo;
}
