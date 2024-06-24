using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = .8f;
    [SerializeField] private float fadeTime = .4f;

    private float baseTransparency = 1f;

    private SpriteRenderer sr;
    private Tilemap tileMap;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        tileMap = GetComponent<Tilemap>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
            if (sr)
                StartCoroutine(FadeCoroutine(sr, fadeTime, sr.color.a, transparencyAmount));
            else if (tileMap)
                StartCoroutine(FadeCoroutine(tileMap, fadeTime, tileMap.color.a, transparencyAmount));
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
            if (sr)
                StartCoroutine(FadeCoroutine(sr, fadeTime, sr.color.a, baseTransparency));
            else if (tileMap)
                StartCoroutine(FadeCoroutine(tileMap, fadeTime, tileMap.color.a, baseTransparency));
    }
    private IEnumerator FadeCoroutine(SpriteRenderer _sr, float _fadeTime, float _startValue, float _targetTransparent)
    {
        float elapsedTime = 0;
        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(_startValue, _targetTransparent, elapsedTime / _fadeTime);
            _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, newAlpha);
            yield return null;
        }
    }
    private IEnumerator FadeCoroutine(Tilemap _tileMap, float _fadeTime, float _startValue, float _targetTransparent)
    {
        float elapsedTime = 0;
        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(_startValue, _targetTransparent, elapsedTime / _fadeTime);
            _tileMap.color = new Color(_tileMap.color.r, _tileMap.color.g, _tileMap.color.b, newAlpha);
            yield return null;
        }
    }
}
