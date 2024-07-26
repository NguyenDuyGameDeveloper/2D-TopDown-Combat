using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickUpType
    {
        GoldCoin, Health, Stamina
    }
    #region Info
    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float coinMoveSpeed = 3f;
    [SerializeField] private float accelarationRate = .2f;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;
    [SerializeField] private AnimationCurve animCurve;

    private Vector3 moveDir;
    private Rigidbody2D rb;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        StartCoroutine(AnimCurveSpawnCoroutine());
    }
    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            coinMoveSpeed += accelarationRate;
        }
        else
        {
            moveDir = Vector3.zero;
            coinMoveSpeed = 0f;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = moveDir * coinMoveSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            DetectPickUpType();
            Destroy(gameObject);
        }
    }
    private IEnumerator AnimCurveSpawnCoroutine()
    {
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);
        float timePassed = 0f;

        Vector2 startPoint = transform.position;
        Vector2 endPoint = new Vector2(randomX, randomY);

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
        //while(timePassed < popDuration)
        //{
        //    //Vector2 startPoint,endPoint;
        //    timePassed += Time.deltaTime;
        //    float linearT = timePassed / popDuration;
        //    float heightT = animCurve.Evaluate(linearT);
        //    float height = Mathf.Lerp(0f,heightY,heightT);

        //    transform.position= Vector2.Lerp(transform.position,
        //        new Vector2(Random.Range(-2f,2f),Random.Range(-2f,2f)),linearT);
        //    yield return null;
        //}
    }
    private void DetectPickUpType()
    {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                break;
            case PickUpType.Health:
                PlayerHealth.Instance.HealPlayer();
                break;
            case PickUpType.Stamina:
                Stamina.Instance.RestoreStamina();
                break;
            default: break;
        }
    }
}
