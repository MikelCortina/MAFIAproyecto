using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class CambioDeEscena : MonoBehaviour
{
    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene("SampleScene");
    }
}

