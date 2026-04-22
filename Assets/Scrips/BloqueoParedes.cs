using UnityEngine;

public class LimiteInvisible : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Intentó salir del museo");

            // Lo devuelve al centro (puedes cambiar esto)
            other.transform.position = new Vector3(4, 0, -5);
        }
    }
}