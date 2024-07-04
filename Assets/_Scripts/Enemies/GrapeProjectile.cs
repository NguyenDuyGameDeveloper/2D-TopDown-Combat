using System.Collections;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    #region Info
    [SerializeField] private float duration = 1f;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private GameObject grapeProjectileShadowPrefab;
    [SerializeField] private GameObject splatterPrefab;
    #endregion
    private void Start()
    {
        GameObject grapeShadow = Instantiate(grapeProjectileShadowPrefab,
            transform.position + new Vector3(0, -0.3f, 0), Quaternion.identity);

        Vector3 playerPos = PlayerController.Instance.transform.position;

        StartCoroutine(ProjectileCurveCoroutine(transform.position, playerPos));
        StartCoroutine(MoveGrapeShadowCoroutine(grapeShadow, transform.position, playerPos));
    }
    private IEnumerator ProjectileCurveCoroutine(Vector3 _startPosition, Vector3 _endPosition)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = 
                Vector2.Lerp(_startPosition, _endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }
        Instantiate(splatterPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    private IEnumerator MoveGrapeShadowCoroutine(GameObject _grapeShadow, Vector3 _startPosition, Vector3 _endPosition)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;

            _grapeShadow.transform.position = 
                Vector2.Lerp(_startPosition, _endPosition, linearT);

            yield return null;
        }
        Destroy(_grapeShadow);
    }
}
