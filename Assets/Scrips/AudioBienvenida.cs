using UnityEngine;

public class AudioBienvenida : MonoBehaviour
{
    public AudioSource audioBienvenida;

    void Start()
    {
        audioBienvenida.Play();
    }
}