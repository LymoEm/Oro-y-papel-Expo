using UnityEngine;
using UnityEngine.InputSystem;

public class InteraccionJugador : MonoBehaviour
{
    public float distancia = 3f;
    public Camera camara;

    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Ray rayo = camara.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit impacto;

            if (Physics.Raycast(rayo, out impacto, distancia))
            {
                ObjetoInteractivo obj = impacto.collider.GetComponent<ObjetoInteractivo>();

                if (obj != null)
                {
                    obj.Interactuar();
                }
            }
        }
    }
}