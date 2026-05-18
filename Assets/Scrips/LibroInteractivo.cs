using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LibroInteractivo : MonoBehaviour
{
    [Header("Cámara jugador")]
    public Camera camaraJugador;

    public Transform puntoEnfoque;

    public float velocidadCamara = 4f;

    private Vector3 posicionOriginal;

    private Quaternion rotacionOriginal;

    private bool moviendoCamara = false;

    private bool enLibro = false;

    [Header("Movimiento jugador")]
    public MonoBehaviour movimientoJugador;

    [Header("UI")]
    public GameObject botonDescubre;

    public GameObject panelFicha;

    public Button botonCerrar;

    [Header("Narración")]
    public AudioSource audioNarracion;

    public Image narraEmily;

    private bool titilando = false;

    void Start()
    {
        Debug.Log("SCRIPT LIBRO INICIADO");

        panelFicha.SetActive(false);

        botonCerrar.gameObject.SetActive(false);

        botonCerrar.onClick.AddListener(
            CerrarLibro
        );

        Debug.Log("UI LIBRO CONFIGURADA");
    }

    // =========================
    // ABRIR LIBRO
    // =========================
    public void AbrirLibro()
    {
        Debug.Log("ABRIENDO LIBRO");

        if (enLibro)
        {
            Debug.Log("YA ESTABA EN LIBRO");
            return;
        }

        enLibro = true;

        // guardar posición cámara
        posicionOriginal =
            camaraJugador.transform.position;

        rotacionOriginal =
            camaraJugador.transform.rotation;

        Debug.Log("POSICIÓN GUARDADA");

        // mover cámara
        moviendoCamara = true;

        // bloquear movimiento
        if (movimientoJugador != null)
        {
            movimientoJugador.enabled = false;

            Debug.Log(
                "MOVIMIENTO BLOQUEADO"
            );
        }

        // cursor
        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible = true;

        // ocultar descubre
        botonDescubre.SetActive(false);

        // mostrar ficha
        panelFicha.SetActive(true);

        botonCerrar.gameObject.SetActive(true);

        Debug.Log("UI ACTIVADA");

        // iniciar audio
        if (audioNarracion != null)
        {
            audioNarracion.Play();

            Debug.Log("AUDIO INICIADO");
        }

        // titilar
        titilando = true;
    }

    // =========================
    // UPDATE
    // =========================
    void Update()
    {
        // mover cámara
        if (moviendoCamara)
        {
            camaraJugador.transform.position =
                Vector3.Lerp(
                    camaraJugador.transform.position,
                    puntoEnfoque.position,
                    Time.deltaTime *
                    velocidadCamara
                );

            camaraJugador.transform.rotation =
                Quaternion.Lerp(
                    camaraJugador.transform.rotation,
                    puntoEnfoque.rotation,
                    Time.deltaTime *
                    velocidadCamara
                );

            if (
                Vector3.Distance(
                    camaraJugador.transform.position,
                    puntoEnfoque.position
                ) < 0.05f
            )
            {
                moviendoCamara = false;

                Debug.Log(
                    "CÁMARA LLEGÓ AL LIBRO"
                );
            }
        }

        // TITILAR NARRA EMILY
        if (titilando && narraEmily != null)
        {
            Color c = narraEmily.color;

            c.a =
                Mathf.PingPong(
                    Time.time * 2f,
                    1f
                );

            narraEmily.color = c;
        }
    }

    // =========================
    // CERRAR LIBRO
    // =========================
    public void CerrarLibro()
    {
        Debug.Log("CERRANDO LIBRO");

        enLibro = false;

        moviendoCamara = false;

        // restaurar cámara
        camaraJugador.transform.position =
            posicionOriginal;

        camaraJugador.transform.rotation =
            rotacionOriginal;

        Debug.Log("CÁMARA RESTAURADA");

        // restaurar movimiento
        if (movimientoJugador != null)
        {
            movimientoJugador.enabled = true;

            Debug.Log(
                "MOVIMIENTO RESTAURADO"
            );
        }

        // ocultar ficha
        panelFicha.SetActive(false);

        botonCerrar.gameObject.SetActive(false);

        // volver descubre
        botonDescubre.SetActive(true);

        Debug.Log("UI RESTAURADA");

        // detener audio
        if (audioNarracion != null)
        {
            audioNarracion.Stop();

            Debug.Log("AUDIO DETENIDO");
        }

        titilando = false;

        // restaurar alpha narra emily
        if (narraEmily != null)
        {
            Color c = narraEmily.color;

            c.a = 1f;

            narraEmily.color = c;
        }

        Cursor.lockState =
            CursorLockMode.Locked;

        Cursor.visible = false;
    }
}