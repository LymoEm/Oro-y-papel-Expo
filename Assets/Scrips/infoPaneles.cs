using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class infoPaneles : MonoBehaviour
{
    [Header("Cámara")]
    public Camera mainCamera;

    public Transform cameraFocusPoint;

    public float cameraSpeed = 4f;

    private Vector3 originalCamPos;

    private Quaternion originalCamRot;

    [Header("Jugador")]
    public MonoBehaviour playerMovement;

    [Header("UI")]
    public GameObject panelUI;

    public Button botonSaberMas;

    public Button botonDatoCurioso;

    public Button botonCerrar;

    [Header("Contenido UI")]
    public GameObject imagenInicial;

    public GameObject contenedorTexto;

    public TMP_Text textoUI;

    [TextArea]
    public string textoCompleto;

    public float velocidadTexto = 0.03f;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip audioDatoCurioso;

    private Collider miCollider;

    private bool enInteraccion = false;

    private bool moviendoCamara = false;

    private Coroutine typingCoroutine;

    void Start()
    {
        Debug.Log("Start ejecutado");

        miCollider = GetComponent<Collider>();

        contenedorTexto.SetActive(false);

        botonCerrar.gameObject.SetActive(false);

        imagenInicial.SetActive(true);

        textoUI.text = "";

        Debug.Log("UI inicializada");

        // BOTONES
        botonSaberMas.onClick.AddListener(
            ActivarTexto
        );

        botonDatoCurioso.onClick.AddListener(
            ReproducirAudio
        );

        botonCerrar.onClick.AddListener(
            CerrarPanel
        );

        Debug.Log("Botones conectados");
    }

    public void Interactuar()
    {
        Debug.Log("INTERACTUAR");

        if (enInteraccion)
        {
            Debug.Log("Ya estaba interactuando");
            return;
        }

        enInteraccion = true;

        //desactivar collider
        if (miCollider != null)
        {
            miCollider.enabled = false;

            Debug.Log("Collider desactivado");
        }

        // guardar posición ACTUAL
        originalCamPos =
            mainCamera.transform.position;

        originalCamRot =
            mainCamera.transform.rotation;

        Debug.Log("Posición actual guardada");

        moviendoCamara = true;

        Debug.Log("Cámara moviéndose");

        // bloquear jugador
        if (playerMovement != null)
        {
            playerMovement.enabled = false;

            Debug.Log("Movimiento bloqueado");
        }
        else
        {
            Debug.LogError(
                "playerMovement NO asignado"
            );
        }

        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible = true;

        botonCerrar.gameObject.SetActive(true);

        // LA IMAGEN SIGUE VISIBLE
        imagenInicial.SetActive(true);

        contenedorTexto.SetActive(false);

        textoUI.text = "";

        Debug.Log("Interacción iniciada");
    }

    void Update()
    {
        if (moviendoCamara)
        {
            mainCamera.transform.position =
                Vector3.Lerp(
                    mainCamera.transform.position,
                    cameraFocusPoint.position,
                    Time.deltaTime * cameraSpeed
                );

            mainCamera.transform.rotation =
                Quaternion.Lerp(
                    mainCamera.transform.rotation,
                    cameraFocusPoint.rotation,
                    Time.deltaTime * cameraSpeed
                );

            if (
                Vector3.Distance(
                    mainCamera.transform.position,
                    cameraFocusPoint.position
                ) < 0.05f
            )
            {
                moviendoCamara = false;

                Debug.Log(
                    "Cámara llegó al panel"
                );
            }
        }
    }

    public void ActivarTexto()
    {
        Debug.Log("BOTÓN SABER MÁS");

        imagenInicial.SetActive(false);

        contenedorTexto.SetActive(true);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine =
            StartCoroutine(
                EfectoMaquinaEscribir()
            );

        Debug.Log("Texto activado");
    }

    IEnumerator EfectoMaquinaEscribir()
    {
        Debug.Log("Escribiendo texto");

        textoUI.text = "";

        foreach (char letra in textoCompleto)
        {
            textoUI.text += letra;

            yield return new WaitForSeconds(
                velocidadTexto
            );
        }

        Debug.Log("Texto terminado");
    }

    public void ReproducirAudio()
    {
        Debug.Log("BOTÓN DATO CURIOSO");

        if (
            audioSource != null &&
            audioDatoCurioso != null
        )
        {
            audioSource.Stop();

            audioSource.clip =
                audioDatoCurioso;

            audioSource.Play();

            Debug.Log(
                "Audio reproduciéndose"
            );
        }
        else
        {
            Debug.LogError(
                "Falta AudioSource o AudioClip"
            );
        }
    }

    public void CerrarPanel()
    {
        Debug.Log("CERRANDO PANEL");

        enInteraccion = false;

        moviendoCamara = false;

        mainCamera.transform.position =
            originalCamPos;

        mainCamera.transform.rotation =
            originalCamRot;

        Debug.Log("Cámara restaurada");

        if (playerMovement != null)
        {
            playerMovement.enabled = true;

            Debug.Log(
                "Movimiento restaurado"
            );
        }


        contenedorTexto.SetActive(false);

        imagenInicial.SetActive(true);

        textoUI.text = "";

        botonCerrar.gameObject.SetActive(false);

        Debug.Log("UI restaurada");

        if (audioSource != null)
        {
            audioSource.Stop();

            Debug.Log("Audio detenido");
        }

        //volver a activar collider
        if (miCollider != null)
        {
            miCollider.enabled = true;

            Debug.Log("Collider activado otra vez");
        }
    }
}