using UnityEngine;

public class PanelInteractivo : MonoBehaviour
{
    [Header("Configuración")]
    public Transform camaraJugador;

    public float distanciaFrente = 1.5f;

    public float escalaAgrandada = 3f;

    public GameObject botonCerrarUI;

    private Vector3 posicionOriginal;
    private Quaternion rotacionOriginal;
    private Vector3 escalaOriginal;

    private bool estaAgrandado = false;

    void Start()
    {
        posicionOriginal = transform.position;
        rotacionOriginal = transform.rotation;
        escalaOriginal = transform.localScale;

        botonCerrarUI.SetActive(false);
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

        transform.position =
            camaraJugador.position +
            camaraJugador.forward * distanciaFrente;

        transform.LookAt(camaraJugador);

        transform.Rotate(0, 180, 0);

        transform.localScale =
            escalaOriginal * escalaAgrandada;

        GameObject jugador =
            GameObject.Find("Jugador");

        if (jugador != null)
        {
            jugador.GetComponent<MovimientoJugador>().enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        botonCerrarUI.SetActive(true);
    }

    public void Restaurar()
    {
        estaAgrandado = false;

        transform.position = posicionOriginal;

        transform.rotation = rotacionOriginal;

        transform.localScale = escalaOriginal;

        GameObject jugador =
            GameObject.Find("Jugador");

        if (jugador != null)
        {
            jugador.GetComponent<MovimientoJugador>().enabled = true;
        }

        botonCerrarUI.SetActive(false);
    }
}