using UnityEngine;

public class ZonaProhibida : MonoBehaviour
{
    [Header("A dónde devolver")]
    public Transform puntoSeguro;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entró en zona prohibida");

            other.transform.position = puntoSeguro.position;
        }
    }
}