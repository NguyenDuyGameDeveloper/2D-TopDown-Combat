using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeScreen : Singleton<UI_FadeScreen>
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed = 1f;

    private IEnumerator fadeScreenCoroutine;

    public void FadeToBlack()
    {
        if (fadeScreenCoroutine != null)
            StopCoroutine(fadeScreenCoroutine);

        fadeScreenCoroutine = FadeScreenCoroutine(1);
        StartCoroutine(fadeScreenCoroutine);
    }
    public void FadeToClear()
    {
        if (fadeScreenCoroutine != null)
            StopCoroutine(fadeScreenCoroutine);

        fadeScreenCoroutine = FadeScreenCoroutine(0);
        StartCoroutine(fadeScreenCoroutine);
    }
    private IEnumerator FadeScreenCoroutine(float _targetAlpha)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, _targetAlpha))
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, _targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
    }
}
