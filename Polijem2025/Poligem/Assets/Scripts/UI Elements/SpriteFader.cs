using System.Collections;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] float minAlpha = 0f;
    [SerializeField] float maxAlpha = 1f;

    private Coroutine fadeCoroutine;

    void Start ()
    {
        if (spriteRenderer != null)
        {
            Color initialColor = spriteRenderer.color;
            initialColor.a = minAlpha;
            spriteRenderer.color = initialColor;
        }
    }

    public void FadeIn ()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine (fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine (FadeToAlpha (maxAlpha));
    }

    public void FadeOut ()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine (fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine (FadeToAlpha (minAlpha));
    }

    private IEnumerator FadeToAlpha (float targetAlpha)
    {
        if (spriteRenderer == null)
        {
            yield break;
        }

        Color color = spriteRenderer.color;
        float startAlpha = color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp (startAlpha, targetAlpha, elapsed / fadeDuration);
            spriteRenderer.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        spriteRenderer.color = color;
    }
}
