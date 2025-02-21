using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioEscena : MonoBehaviour

{
    public GameObject panel1;  // El primer panel
    public GameObject panel2;  // El segundo panel

    // M�todo para mostrar el panel 2 y ocultar el panel 1
    public void MostrarPanel2()
    {
        panel1.SetActive(false); // Desactiva el panel 1
        panel2.SetActive(true);  // Activa el panel 2
    }

    // M�todo para mostrar el panel 1 y ocultar el panel 2
    public void MostrarPanel1()
    {
        panel2.SetActive(false); // Desactiva el panel 2
        panel1.SetActive(true);  // Activa el panel 1
    }
}


