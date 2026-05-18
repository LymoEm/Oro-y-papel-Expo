using UnityEngine;

public class GuardarPosicion : MonoBehaviour
{
    public static Vector3 ultimaPosicionValida;

    void Update()
    {
        ultimaPosicionValida = transform.position;
    }
}