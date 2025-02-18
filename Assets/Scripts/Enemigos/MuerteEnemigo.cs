using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float vidaEnemigo = 99f;
    public float vidaEnemigoTotal = 99f;
    public EnemyFSM enemy;
    public Image barraDeVidaRelleno; // Referencia a la barra de vida
    public Transform barraVidaPosicion; // Posición donde se mostrará la barra de vida


    void Start()
    {
        // Ocultar la barra de vida al inicio
        if (barraDeVidaRelleno != null)
        {
            barraDeVidaRelleno.fillAmount = 1; // Barra llena
            barraDeVidaRelleno.gameObject.SetActive(false); // Ocultar barra
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (collision.CompareTag("Bullet"))
        {
            if (!enemy.coberturaEstado)
            {
                // Reducir la vida del enemigo
                float vidaAnterior = vidaEnemigo;
                vidaEnemigo -= bullet.damage;
                Debug.Log("El enemigo tiene " + vidaEnemigo);

                // Mostrar la barra de vida
                MostrarBarraVida();

                // Actualizar la barra de vida
                if (barraDeVidaRelleno != null)
                {
                    barraDeVidaRelleno.fillAmount = vidaEnemigo / vidaEnemigoTotal; // Actualizar proporcionalmente
                }

                // Destruir la bala
                Destroy(collision.gameObject);

                // Verificar si el enemigo muere
                if (vidaEnemigo <= 0)
                {
                     if (barraDeVidaRelleno != null)
                     {
                          barraDeVidaRelleno.gameObject.SetActive(false); // Ocultar barra en vez de destruirla
                     } 
                    Destroy(gameObject);
                     Debug.Log("El enemigo ha muerto");
                }
            }

            else
            {
                Debug.Log("El enemigo está cubierto!");
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Destructor"))
        {
            Destroy(gameObject);
            Debug.Log("El enemigo ha muerto");
        }
    }
    private void MostrarBarraVida()
    {
        if (barraDeVidaRelleno != null)
        {
            // Mostrar la barra de vida
            barraDeVidaRelleno.gameObject.SetActive(true);

        }
    }

    void Update()
    {
        if (this == null) // O si prefieres verificar el transform: if (transform == null)
        {
            Destroy(barraDeVidaRelleno);
            barraDeVidaRelleno.gameObject.SetActive(false);
        }
        // Actualizar la posición de la barra de vida (opcional si el enemigo se mueve)
        if (barraDeVidaRelleno != null && barraVidaPosicion != null)
        {
            barraDeVidaRelleno.transform.position = barraVidaPosicion.position;
        }
    }
}