using UnityEngine;

public class LineToMouse : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        // Obtener o agregar el componente LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Configurar el LineRenderer para evitar que desaparezca
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Material visible
        lineRenderer.sortingLayerName = "Foreground"; // Evitar que se oculte detrás de sprites
        lineRenderer.sortingOrder = 10; // Asegurar que esté por encima de otros elementos
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Mantener el mismo Z del GameObject

        // Establecer los puntos de la línea
        lineRenderer.SetPosition(0, transform.position); // Origen en el GameObject
        lineRenderer.SetPosition(1, mousePosition); // Fin en el mouse
    }
}
