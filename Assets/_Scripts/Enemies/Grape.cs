using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;

    private Animator anim;
    private SpriteRenderer sr;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void Attack()
    {
        anim.SetTrigger(ATTACK_HASH);
        if (transform.position.x - PlayerController.Instance.transform.position.x < 0)
            sr.flipX = false;
        else
            sr.flipX = true;
    }
    public void SpawnProjectileAnimEvent() => Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
}
