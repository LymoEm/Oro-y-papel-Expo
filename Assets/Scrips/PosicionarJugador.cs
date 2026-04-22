using UnityEngine;

public class PosicionarJugador : MonoBehaviour
{
    public Transform puntoInicio;

    void Start()
    {
        transform.position = puntoInicio.position;
        transform.rotation = puntoInicio.rotation;
    }
}