using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Info
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;

    private Vector3 startPosition;
    #endregion
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
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (!collision.isTrigger && (enemyHealth || indestructible || playerHealth))
        {
            if ((playerHealth && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                playerHealth?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
            else if(!collision.isTrigger && indestructible) 
            {

                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
    public void DetecFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
            Destroy(this.gameObject);
    }
    public void UpdateProjectileRange(float _projectileRange) => this.projectileRange = _projectileRange;
    public void UpdateMoveSpeed(float _moveSpeed) => this.moveSpeed = _moveSpeed;
    private void MoveProjectile() => transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
}
