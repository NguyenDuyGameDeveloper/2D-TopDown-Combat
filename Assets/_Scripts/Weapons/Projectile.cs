using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;

    private SO_WeaponInfo weaponInfo;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        MoveProjectile();
        DetecFireDistance();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();

        if (!collision.isTrigger && (enemyHealth || indestructible))
        {
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
    public void DetecFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
            Destroy(this.gameObject);
    }
    public void UpdateWeaponInfo(SO_WeaponInfo _weaponInfo) => this.weaponInfo = _weaponInfo;
    private void MoveProjectile() => transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
}
