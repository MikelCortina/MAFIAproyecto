using UnityEngine;

public class
Cobertura : MonoBehaviour
{
    public bool coberturaEstado = false; // Estado de cobertura
    public GameObject player; // Referencia al jugador
    private Renderer objectRenderer; // Para manejar el color del objeto
    public float energia = 100f;
    public float energiaMaxima = 100f;
    public float consumoEnergiaPorSegundo = 10f;
    public float recuperacionEnergiaPorSegundo = 10f;

    public bool CoberturaEstado => coberturaEstado;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("No se encontró un Renderer en el objeto.");
        }

        if (player == null)
        {
            Debug.LogError("No se asignó un jugador al script.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && energia > 0)
        {
            coberturaEstado = !coberturaEstado;
            Debug.Log(coberturaEstado ? "Estoy cubierto" : "Me he descubierto");
        }

        if (coberturaEstado)
        {
            energia -= Time.deltaTime * consumoEnergiaPorSegundo;
            if (energia <= 0)
            {
                coberturaEstado = false;
            }
        }
        else if (energia < energiaMaxima)
        {
            energia += Time.deltaTime * recuperacionEnergiaPorSegundo;
        }

        if (player != null)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null && Mathf.Abs(playerRb.linearVelocity.x) > 0.3f)
            {
                coberturaEstado = false;
            }
        }

        CambiarEscala(coberturaEstado ? new Vector3(0.88f, 0.5f, 1f) : new Vector3(0.88f, 1f, 1f));
        CambiarColor(Color.white);
    }

    void CambiarColor(Color nuevoColor)
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = nuevoColor;
        }
    }

    void CambiarEscala(Vector3 nuevaEscala)
    {
        transform.localScale = nuevaEscala;
    }
}
