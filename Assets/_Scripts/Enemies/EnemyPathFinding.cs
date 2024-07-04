using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    #region Info
    [SerializeField] private float enemyMoveSpeed = 2f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private KnockBack knockBack;
    private Vector2 enemyMoveDir;
    #endregion

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (knockBack.GettingKnockBack)
            return;

        rb.MovePosition(rb.position + enemyMoveDir * (enemyMoveSpeed * Time.fixedDeltaTime));

        if (enemyMoveDir.x < 0)
            sr.flipX = true;
        else if (enemyMoveDir.x > 0)
            sr.flipX = false;
    }
    public void MoveTo(Vector2 _targetPosition) => enemyMoveDir = _targetPosition;
    public void StopMoving() => enemyMoveDir = Vector3.zero;
}
