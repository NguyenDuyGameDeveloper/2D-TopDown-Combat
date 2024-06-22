using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    #region Info
    private KnockBack knockBack;

    [SerializeField] private float enemyMoveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 enemyMovement;
    #endregion

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (knockBack.GettingKnockBack)
            return;

        rb.MovePosition(rb.position + enemyMovement * (enemyMoveSpeed * Time.fixedDeltaTime));
    }
    public void MoveTo(Vector2 _targetPosition) => enemyMovement = _targetPosition;
}
