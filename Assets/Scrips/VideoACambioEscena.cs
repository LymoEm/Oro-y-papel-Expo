using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoACambioEscena : MonoBehaviour
{
    public VideoPlayer video;
    public string escenaDestino;

    void Start()
    {
        video.loopPointReached += CuandoTermina;
    }

    void CuandoTermina(VideoPlayer vp)
    {
        SceneManager.LoadScene(escenaDestino);
    }
}