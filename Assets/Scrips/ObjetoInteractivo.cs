using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    public string mensaje = "Interactuando";

    public void Interactuar()
    {
        Debug.Log(mensaje);
    }
}