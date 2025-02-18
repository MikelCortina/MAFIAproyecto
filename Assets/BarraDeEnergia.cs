using UnityEngine;
using UnityEngine.UI;

public class BarraDeEnergia : MonoBehaviour
{
    public Cobertura coberturaScript; // Referencia al script Cobertura
    public Image barraDeEnergiaRelleno;

    void Update()
    {
        if (coberturaScript != null)
        {
            // Asegurarte de que la barra represente la proporci�n actual de energ�a
            barraDeEnergiaRelleno.fillAmount = coberturaScript.energia / coberturaScript.energiaMaxima;
        }
    }

    // M�todo opcional para modificar la energ�a desde el script Cobertura
    public void CambiarEnergia(float cantidad)
    {
        if (coberturaScript != null)
        {
            coberturaScript.energia += cantidad;
        }
    }
}
