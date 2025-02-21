using UnityEngine;
using System.Collections;

public class ButttonElevator : MonoBehaviour
{
    public GameObject elevator; // Referencia al objeto del ascensor
    public float moveDistance = 5f; // Distancia que el ascensor se moverá
    public float moveSpeed = 2f;    // Velocidad de movimiento del ascensor

    private Vector3 startPosition;
    private bool isActivated = false;

    void Start()
    {
        if (elevator != null)
        {
            startPosition = elevator.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            StartCoroutine(MoveElevator());
        }
        if (other.CompareTag("Bullet") && !isActivated)
        {
            StartCoroutine(MoveElevator());
        }
        if (other.CompareTag("Dog") && !isActivated)
        {
            StartCoroutine(MoveElevator());
        }
    }

    private IEnumerator MoveElevator()
    {
        isActivated = true;

        // Calculando el ángulo en radianes (-12 grados)
        float angleInRadians = Mathf.Deg2Rad * -12f;

        // Calculando los desplazamientos en X y Y usando trigonometría
        float deltaX = moveDistance * Mathf.Cos(angleInRadians);
        float deltaY = moveDistance * Mathf.Sin(angleInRadians);

        // Nueva posición objetivo con el desplazamiento en diagonal
        Vector3 targetPosition = startPosition + new Vector3(deltaX, deltaY, 0);

        // Moviendo el ascensor hasta la nueva posición objetivo
        while (Vector3.Distance(elevator.transform.position, targetPosition) > 0.01f)
        {
            elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        elevator.transform.position = targetPosition; // Asegurarse de que la posición final es exacta
        isActivated = false;
    }
}
