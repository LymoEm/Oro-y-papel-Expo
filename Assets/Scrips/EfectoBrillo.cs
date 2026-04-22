using UnityEngine;

public class EfectoBrillo : MonoBehaviour
{
    public float velocidad = 2f;

    void Update()
    {
        float intensidad = Mathf.PingPong(Time.time * velocidad, 1f);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow * intensidad);
    }
}