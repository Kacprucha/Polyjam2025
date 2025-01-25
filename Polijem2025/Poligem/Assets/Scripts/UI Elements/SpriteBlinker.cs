using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlinker : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float blinkInterval = 0.5f;
    [SerializeField] float minAlpha = 0.2f;
    [SerializeField] float maxAlpha = 0.6f;

    private bool isBlinking = false;

    // Start is called before the first frame update
    void Start()
    {
        if (sprite != null)
        {
            Color initialColor = sprite.color;
            initialColor.a = maxAlpha;
            sprite.color = initialColor;

            StartBlinking ();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBlinking ()
    {
        if (!isBlinking && sprite != null)
        {
            StartCoroutine (Blinking ());
        }
    }

    private IEnumerator Blinking ()
    {
        isBlinking = true;

        while (isBlinking)
        {
            yield return StartCoroutine (FadeToAlpha (minAlpha));

            yield return StartCoroutine (FadeToAlpha (maxAlpha));
        }
    }

    private IEnumerator FadeToAlpha (float targetAlpha)
    {
        Color color = sprite.color;
        float startAlpha = color.a;

        float elapsed = 0f;
        while (elapsed < blinkInterval)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp (startAlpha, targetAlpha, elapsed / blinkInterval);
            sprite.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        sprite.color = color;
    }
}
