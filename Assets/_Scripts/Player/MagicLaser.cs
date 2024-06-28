using System.Collections;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 2f;

    private SpriteRenderer sr;
    private CapsuleCollider2D cc;

    private float laserRange;
    private bool isGrowing = true;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        LaserFaceMouse();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Indestructible>() && !collision.isTrigger)
            isGrowing = false;
    }
    public void UpdateLaserRange(float _laserRange)
    {
        this.laserRange = _laserRange;
        StartCoroutine(IncreaseLaserLengthCoroutine());
    }
    private void LaserFaceMouse()
    {
        Vector3 mouPosition = Input.mousePosition;
        mouPosition = Camera.main.ScreenToWorldPoint(mouPosition);
        Vector2 direction = transform.position - mouPosition;
        transform.right = -direction;
    }
    private IEnumerator IncreaseLaserLengthCoroutine()
    {
        float timePassed = 0f;
        while (sr.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / this.laserGrowTime;

            sr.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);

            cc.offset = new Vector2((Mathf.Lerp(1f,laserRange,linearT)) / 2, cc.offset.y);
            cc.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), cc.size.y);

            yield return null;
        }
        StartCoroutine(GetComponent<SpriteFade>().SlowFadeCoroutine());
    }
}
