using UnityEngine;
using UnityEngine.InputSystem;

public class PanelInteractivo : MonoBehaviour
{
    [Header("Configuración")]
    public Transform camaraJugador;
    public float distanciaFrente = 1.5f;
    public float escalaAgrandada = 3f;
    public bool tieneBrillo = false;

    private Vector3 posicionOriginal;
    private Quaternion rotacionOriginal;
    private Vector3 escalaOriginal;

    private bool estaAgrandado = false;

    void Start()
    {
        posicionOriginal = transform.position;
        rotacionOriginal = transform.rotation;
        escalaOriginal = transform.localScale;

        if (tieneBrillo)
        {
            ActivarBrillo();
        }
    }

    void Update()
    {
        if (estaAgrandado)
        {
            // Cerrar con ESC o clic derecho
            if (Keyboard.current.escapeKey.wasPressedThisFrame ||
                Mouse.current.rightButton.wasPressedThisFrame)
            {
                VolverNormal();
            }
        }
    }

    public void Interactuar()
    {
        if (!estaAgrandado)
        {
            Agrandar();
        }
    }

    void Agrandar()
    {
        estaAgrandado = true;

        // Mover frente a la cámara
        transform.position = camaraJugador.position + camaraJugador.forward * distanciaFrente;

        // Mirar a la cámara
        transform.LookAt(camaraJugador);

        // Rotación correcta (evita que quede al revés)
        transform.Rotate(0, 180, 0);

        // Escalar
        transform.localScale = escalaOriginal * escalaAgrandada;

        // Bloquear movimiento del jugador
        GameObject jugador = GameObject.Find("Jugador");
        if (jugador != null)
        {
            jugador.GetComponent<MovimientoJugador>().enabled = false;
        }
    }

    void VolverNormal()
    {
        estaAgrandado = false;

        transform.position = posicionOriginal;
        transform.rotation = rotacionOriginal;
        transform.localScale = escalaOriginal;

        // Activar movimiento otra vez
        GameObject jugador = GameObject.Find("Jugador");
        if (jugador != null)
        {
            jugador.GetComponent<MovimientoJugador>().enabled = true;
        }
    }

    void ActivarBrillo()
    {
        Renderer r = GetComponent<Renderer>();
        if (r != null)
        {
            r.material.EnableKeyword("_EMISSION");
            r.material.SetColor("_EmissionColor", Color.yellow * 2f);
        }
    }
}
