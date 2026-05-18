using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoACambioEscena : MonoBehaviour
{
    public VideoPlayer video;
    public string escenaDestino;

    public TransicionPantalla transicion;

    private bool cambiando = false;

    void Start()
    {
        video.loopPointReached += CuandoTermina;
    }

    void CuandoTermina(VideoPlayer vp)
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

        SceneManager.LoadScene(escenaDestino);
    }
}