using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float sensibilidadMouse = 0.2f;
    public Transform camaraJugador;

    private Vector2 movimientoInput;
    private Vector2 mouseInput;

    float rotacionX = 0f;

    void Start()
    {
        
    }

    void Update()
    {

        Mover();
        Mirar();
    }

    public void OnMove(InputValue value)
    {
        movimientoInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        mouseInput = value.Get<Vector2>();
    }

    void Mover()
    {
        Vector3 movimiento = transform.right * movimientoInput.x + transform.forward * movimientoInput.y;
        transform.position += movimiento * velocidad * Time.deltaTime;
    }

    void Mirar()
    {
        float mouseX = mouseInput.x * sensibilidadMouse * 100f * Time.deltaTime;
        float mouseY = mouseInput.y * sensibilidadMouse * 100f * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        camaraJugador.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}