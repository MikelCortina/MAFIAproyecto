using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaTotal : MonoBehaviour
{
    public PlayerMovement vidaScript; // Referencia al script Cobertura
    public Image barraDeVidaRelleno;

    void Update()
    {
        if (vidaScript != null)
        {
            // Asegurarte de que la barra represente la proporci�n actual de energ�a
            barraDeVidaRelleno.fillAmount = vidaScript.vidaActual/ vidaScript.vidaTotal;
        }
    }

    // M�todo opcional para modificar la energ�a desde el script Cobertura
    public void CambiarEnergia(float cantidad)
    {
        if (vidaScript != null)
        {
            vidaScript.vidaTotal += cantidad;
        }
    }
}
