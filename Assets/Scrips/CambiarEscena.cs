using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public string nombreEscena;

    public void Cambiar()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}