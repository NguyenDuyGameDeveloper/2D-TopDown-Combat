using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool GettingKnockBack { get; private set; }
    private Rigidbody2D rb;

    [SerializeField] private float knockBackTime = .2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetKnockBack(Transform _damageSource, float _knockBackThrust)
    {
        GettingKnockBack = true;
        Vector2 different = (transform.position - _damageSource.position).normalized * _knockBackThrust * rb.mass;
        rb.AddForce(different, ForceMode2D.Impulse);
        StartCoroutine(KnockBackCoroutine());
    }
    private IEnumerator KnockBackCoroutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockBack = false;
    }
}
