using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransicionPantalla : MonoBehaviour
{
    public Image fadeImage;

    [Header("Duración")]
    public float duracionFade = 1.5f;

    [Header("Fade automático al iniciar")]
    public bool hacerFadeInAlIniciar = false;

    void Start()
    {
        if (hacerFadeInAlIniciar)
        {
            StartCoroutine(FadeIn());
        }
    }

    public IEnumerator FadeOut()
    {
        float tiempo = 0;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;

            float alpha = tiempo / duracionFade;

            fadeImage.color = new Color(0, 0, 0, alpha);

            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        float tiempo = duracionFade;

        while (tiempo > 0)
        {
            tiempo -= Time.deltaTime;

            float alpha = tiempo / duracionFade;

            fadeImage.color = new Color(0, 0, 0, alpha);

            yield return null;
        }
    }
}