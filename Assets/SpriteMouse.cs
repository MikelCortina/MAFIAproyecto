using Unity.Burst.CompilerServices;
using UnityEngine;

public class MouseCursorChanger : MonoBehaviour
{
    public Texture2D defaultCursor;   // Cursor normal
    public Texture2D enemyCursor;     // Cursor para enemigos
    public Texture2D objectCursor;    // Cursor para objetos
    public Vector2 cursorHotspot = Vector2.zero; // Punto de anclaje del cursor

    void Start()
    {
        Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
    }

    void Update()
    {
        // Convertir la posición del mouse a coordenadas del mundo
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Creamos el rayo desde el ratón
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction); // Raycast en 2D

        if (hit.collider != null)
        {
            // Verificamos si el rayo ha golpeado un objeto con la etiqueta "Enemigo"
            if (hit.collider != null && hit.collider.CompareTag("Enemigo"))
            {
                Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);
                Debug.Log("Enemigo");
            }
           else if (hit.collider != null && hit.collider.CompareTag("Interruptor"))
            {
                Cursor.SetCursor(objectCursor, cursorHotspot, CursorMode.Auto);
                Debug.Log("Objeto");
            }
            else
            {
                Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
        }
    }
   

     

}
