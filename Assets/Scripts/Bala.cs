using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 10000f;  // Duraci�n de vida del proyectil
    public float speed = 20f;    // Velocidad del proyectil
    private Rigidbody2D rb;
    public float damage;// Referencia al Rigidbody2D

    void Start()
    {
        // Obt�n el componente Rigidbody2D de la bala
        rb = GetComponent<Rigidbody2D>();

        // Si el Rigidbody no tiene una velocidad inicial, dale una
        // Esto solo se usar� si la velocidad no se estableci� en el script de disparo
        // Destruye la bala despu�s de un tiempo determinado
        Destroy(gameObject, lifeTime);
    }

  
}
