using UnityEngine;
using UnityEngine.UI;
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
    public Text textoUI;

    [TextArea]
    public string textoCompleto;
    public float velocidadTexto = 0.03f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip audioDatoCurioso;

    private bool enInteraccion = false;
    private bool moviendoCamara = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        originalCamPos = mainCamera.transform.position;
        originalCamRot = mainCamera.transform.rotation;

        panelUI.SetActive(false);
        contenedorTexto.SetActive(false);
        botonCerrar.gameObject.SetActive(false);

        botonSaberMas.onClick.AddListener(ActivarTexto);
        botonDatoCurioso.onClick.AddListener(ReproducirAudio);
        botonCerrar.onClick.AddListener(CerrarPanel);
    }

    // 🔥 ESTE ES EL MÉTODO QUE LLAMA EL RAYCAST
    public void Interactuar()
    {
        if (enInteraccion) return;

        enInteraccion = true;
        moviendoCamara = true;

        // Bloquear jugador
        if (playerMovement != null)
            playerMovement.enabled = false;

        // Activar cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        panelUI.SetActive(true);
        botonCerrar.gameObject.SetActive(true);

        imagenInicial.SetActive(true);
        contenedorTexto.SetActive(false);
        textoUI.text = "";
    }

    void Update()
    {
        if (moviendoCamara)
        {
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                cameraFocusPoint.position,
                Time.deltaTime * cameraSpeed
            );

            mainCamera.transform.rotation = Quaternion.Lerp(
                mainCamera.transform.rotation,
                cameraFocusPoint.rotation,
                Time.deltaTime * cameraSpeed
            );

            if (Vector3.Distance(mainCamera.transform.position, cameraFocusPoint.position) < 0.05f)
            {
                moviendoCamara = false;
            }
        }
    }

    void ActivarTexto()
    {
        imagenInicial.SetActive(false);
        contenedorTexto.SetActive(true);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(EfectoMaquinaEscribir());
    }

    IEnumerator EfectoMaquinaEscribir()
    {
        textoUI.text = "";

        foreach (char letra in textoCompleto)
        {
            textoUI.text += letra;
            yield return new WaitForSeconds(velocidadTexto);
        }
    }

    void ReproducirAudio()
    {
        if (audioSource && audioDatoCurioso)
        {
            audioSource.Stop();
            audioSource.clip = audioDatoCurioso;
            audioSource.Play();
        }
    }

    void CerrarPanel()
    {
        enInteraccion = false;
        moviendoCamara = false;

        mainCamera.transform.position = originalCamPos;
        mainCamera.transform.rotation = originalCamRot;

        if (playerMovement != null)
            playerMovement.enabled = true;

        // 🔥 VOLVER A MODO FPS
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        panelUI.SetActive(false);
        contenedorTexto.SetActive(false);
        imagenInicial.SetActive(true);

        if (audioSource)
            audioSource.Stop();

        textoUI.text = "";
    }
}