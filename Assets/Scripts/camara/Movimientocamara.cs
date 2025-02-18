using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // Referencia al Transform del jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado de la c�mara
    public Vector3 offset;          // Desplazamiento de la c�mara respecto al jugador

    void LateUpdate()
    {
        // Solo seguimos al jugador en el eje X (manteniendo las posiciones Y y Z fijas)
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);

        // Interpolamos suavemente hacia la posici�n deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplicamos la posici�n suavizada a la c�mara
        transform.position = smoothedPosition;
    }
}
