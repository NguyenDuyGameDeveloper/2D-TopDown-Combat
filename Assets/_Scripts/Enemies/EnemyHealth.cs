using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 10f;

    private KnockBack knockBack;
    private Flash flash;
    private int currentHealth;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }
    private void Start()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashCoroutine());
        StartCoroutine(DieCoroutine());
    }
    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        Die();
    }
    public void Die()
    {
        if(currentHealth <= 0)
        {
            GetComponent<PickupSpawner>().DropItem();
            Instantiate(deathVFXPrefab,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
