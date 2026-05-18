using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public string nombreEscena;

    public TransicionPantalla transicion;

    private bool cambiando = false;

    public void Cambiar()
    {
        if (!cambiando)
        {
            StartCoroutine(CambiarConFade());
        }
    }

    IEnumerator CambiarConFade()
    {
        cambiando = true;

        yield return StartCoroutine(transicion.FadeOut());

        SceneManager.LoadScene(nombreEscena);
    }
}