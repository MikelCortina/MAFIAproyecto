using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float visionRange = 10f;  // Rango m�ximo del campo de visi�n
    public float visionAngle = 60f;  // �ngulo del campo de visi�n (en grados)
    public LayerMask playerLayer;  // Capa del jugador (para filtrar las detecciones)
    public Transform player; // Referencia al jugador

    private bool playerInSight = false; // Para saber si el jugador est� dentro del campo de visi�n

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        // Obtener la direcci�n hacia el jugador desde el enemigo
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // El enemigo mira hacia la izquierda, por lo tanto invertimos la direcci�n en la que est� mirando
        Vector2 enemyLookDirection = -transform.right; // Esto hace que el enemigo mire a la izquierda

        // Comprobar si el jugador est� dentro del �ngulo de visi�n
        float angle = Vector2.Angle(enemyLookDirection, directionToPlayer);  // Comparar el �ngulo de visi�n del enemigo hacia la izquierda

        // Si el �ngulo est� dentro del rango de visi�n y la distancia es correcta, realizar el raycast
        if (angle < visionAngle / 2)
        {
            // Raycast hacia el jugador para ver si est� dentro del campo de visi�n
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, visionRange, playerLayer);  // Lanzamos el raycast hacia la izquierda

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                if (!playerInSight) // Si el jugador entra en el campo de visi�n
                {
                    Debug.Log("Jugador detectado dentro del campo de visi�n a la izquierda");
                    playerInSight = true; // Marcamos que el jugador est� dentro del campo de visi�n
                }
            }
            else
            {
                if (playerInSight) // Si el jugador sale del campo de visi�n
                {
                    Debug.Log("Jugador sali� del campo de visi�n");
                    playerInSight = false; // Marcamos que el jugador sali� del campo de visi�n
                }
            }
        }
        else
        {
            if (playerInSight) // Si el jugador sale del campo de visi�n
            {
                Debug.Log("Jugador sali� del campo de visi�n");
                playerInSight = false; // Marcamos que el jugador sali� del campo de visi�n
            }
        }

       
    }
    private void OnDrawGizmos()
    {
        // Dibujar el campo de visi�n en forma de cono
        Gizmos.color = Color.red;

        // Dibujar un cono de visi�n desde la posici�n del enemigo
        Vector3 left = transform.position + (Quaternion.Euler(0, 0, visionAngle / 2) * -transform.right) * visionRange;
        Vector3 right = transform.position + (Quaternion.Euler(0, 0, -visionAngle / 2) * -transform.right) * visionRange;

        Gizmos.DrawLine(transform.position, left);
        Gizmos.DrawLine(transform.position, right);

        // Dibujar el raycast hacia la izquierda
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, -transform.right * visionRange);
    }
}
