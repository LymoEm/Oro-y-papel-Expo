using UnityEngine;

public class FlechaLuminosa : MonoBehaviour
{
    public Color colorBrillo = Color.yellow;
    public float intensidad = 5f;
    public float velocidad = 3f;

    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;

        mat.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        float pulso = Mathf.Abs(Mathf.Sin(Time.time * velocidad));
        mat.SetColor("_EmissionColor", colorBrillo * pulso * intensidad);
    }
}