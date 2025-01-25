using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListElement : MonoBehaviour
{
    [SerializeField] Text counter;
    [SerializeField] Image buttonIcon;

    protected float fadeDuration = 0.5f;
    protected float minAlpha = 0f;
    protected float maxAlpha = 1f;

    private int collected = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonIcon != null)
        {
            Color color = buttonIcon.color;
            color.a = 0;
            buttonIcon.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCounter ()
    {
        collected++;
        counter.text = $"{collected}/5";

        if (collected == 5)
        {
            StartCoroutine (fadeToAlpha (1));
        }
    }

    private IEnumerator fadeToAlpha (float targetAlpha)
    {
        if (buttonIcon == null)
        {
            yield break;
        }

        Color color = buttonIcon.color;
        float startAlpha = color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp (startAlpha, targetAlpha, elapsed / fadeDuration);
            buttonIcon.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        buttonIcon.color = color;
    }
}
