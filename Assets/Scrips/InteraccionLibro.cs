using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteraccionLibro : MonoBehaviour
{
    [Header("Cámaras")]
    public Camera camaraJugador;
    public Camera camaraLibro;

    [Header("UI")]
    public GameObject canvasDescubre;
    public GameObject canvasFicha;

    [Header("Audio")]
    public AudioSource audioNarracion;

    [Header("Botón Narra Emily")]
    public Image botonNarraEmily;

    private Coroutine titilarCoroutine;

    void Start()
    {
        canvasFicha.SetActive(false);

        camaraLibro.gameObject.SetActive(false);
    }

    public void AbrirLibro()
    {
        // Oculta descubre
        canvasDescubre.SetActive(false);

        // Activa ficha
        canvasFicha.SetActive(true);

        // Cambia cámara
        camaraJugador.gameObject.SetActive(false);
        camaraLibro.gameObject.SetActive(true);

        // Reproduce audio
        audioNarracion.Play();

        // Empieza titilar
        titilarCoroutine = StartCoroutine(TitilarBoton());
    }

    public void CerrarLibro()
    {
        // Reactiva cámara jugador
        camaraJugador.gameObject.SetActive(true);

        // Desactiva cámara libro
        camaraLibro.gameObject.SetActive(false);

        // Oculta ficha
        canvasFicha.SetActive(false);

        // Vuelve descubre
        canvasDescubre.SetActive(true);

        // Detiene audio
        audioNarracion.Stop();

        // Detiene titilar
        if (titilarCoroutine != null)
        {
            StopCoroutine(titilarCoroutine);
        }

        botonNarraEmily.color =
        new Color(1, 1, 1, 1);
    }

    IEnumerator TitilarBoton()
    {
        while (true)
        {
            Color c = botonNarraEmily.color;

            c.a = 0.3f;
            botonNarraEmily.color = c;

            yield return new WaitForSeconds(0.5f);

            c.a = 1f;
            botonNarraEmily.color = c;

            yield return new WaitForSeconds(0.5f);
        }
    }
}