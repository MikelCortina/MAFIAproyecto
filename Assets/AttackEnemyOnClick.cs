using UnityEngine;
using System.Collections;

public class AttackEnemyOnClick : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    private float targetX; // Coordenada X del clic derecho
    private bool isMovingToClick = false; // Indica si el objeto se est� moviendo al punto de clic
    private float initialY; // Coordenada Y inicial del objeto
    public bool seguiraJugador = true;
 

    private Transform target; // Referencia al enemigo o bot�n objetivo
    private EnemyFSM.EnemyState targetStatus;
    private bool objetivoFijado = false;


    private float lastAttackTime = -10f; // Tiempo del �ltimo ataque (inicialmente muy lejos en el pasado)
    public float attackCooldown = 10f; // Tiempo m�nimo entre ataques en segundos

    private bool isButtonTarget = false; // Indica si el objetivo actual es un bot�n
    private EnemyFSM enemy;

    private float lastClickTime = 0f;  // Tiempo del �ltimo clic
    private float doubleClickDelay = 0.3f;
    public Animator animator;

    void Start()
    {
        // Guardar la posici�n Y inicial
        initialY = transform.position.y;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && IsMouseOverEnemy() && CanShoot() && targetStatus != EnemyFSM.EnemyState.Attack)
        {
            objetivoFijado = true;
            isButtonTarget = false; // Es un enemigo
        }
        if (Input.GetMouseButtonDown(1) && IsMouseOverButton() && CanShoot())
        {
            objetivoFijado = true;
            isButtonTarget = true; // Es un bot�n
        }
        if (CheckCooldown() )
        {
            MoveTowardsTarget();
        }

        if (isMovingToClick) 
        { 
            seguiraJugador=false;
        }

        if (isMovingToClick)
        {
            MoveToClickPosition();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seguiraJugador = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            // Tiempo actual desde el �ltimo clic
            float timeSinceLastClick = Time.time - lastClickTime;

            // Si el tiempo entre clics es menor al retraso definido, se considera doble clic
            if (timeSinceLastClick <= doubleClickDelay&&target==null)
            {
               MoveToPoint();
            
        }

            // Actualizar el tiempo del �ltimo clic
            lastClickTime = Time.time;
        }
    }
    private bool CheckCooldown()
    {
        return (Time.time - lastAttackTime >= attackCooldown && objetivoFijado);
    }
    private bool CheckCooldown1()
    {
        return (Time.time - lastAttackTime >= attackCooldown);
    }

    private void MoveToClickPosition()
    {
        // Mover al objeto hacia la posici�n X del clic manteniendo la posici�n Y inicial
        Vector3 targetPosition = new Vector3(targetX, initialY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Verificar si el objeto lleg� al punto del clic
        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {
            isMovingToClick = false; // Detener el movimiento
        }
    }
    bool IsMouseOverEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Creamos el rayo desde el rat�n
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction); // Raycast en 2D

        // Verificamos si el rayo ha golpeado un objeto con la etiqueta "Enemigo"
        if (!objetivoFijado && CheckCooldown1())
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemigo"))
            {
                Debug.Log("Estoy sobre enemigo: " + hit.collider.name);

                // Obtener el script del enemigo
                //  EnemyFSM enemy = hit.collider.GetComponent<EnemyFSM>();

                // Asignar el enemigo como objetivo
                target = hit.collider.transform;
                targetStatus = target.GetComponent<EnemyFSM>().currentState;
            }
            return true;
        }

        return false;
    }

    bool IsMouseOverButton()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Creamos el rayo desde el rat�n
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction); // Raycast en 2D

        // Verificamos si el rayo ha golpeado un objeto con la etiqueta "Interruptor"
        if (!objetivoFijado && CheckCooldown1())
        {
            if (hit.collider != null && hit.collider.CompareTag("Interruptor"))
            {
                Debug.Log("Estoy sobre bot�n: " + hit.collider.name);
                target = hit.collider.transform; // Asignar el bot�n como objetivo
                return true; // Si es un bot�n, retornamos true
            }
        }

        return false;
    }

    private void MoveTowardsTarget()
    {
        if (target == null)
        {
            objetivoFijado = false;
            return; // Salir si no hay objetivo
        }
        animator.SetTrigger("Walk");
        // Calcular el paso de movimiento
        float step = speed * Time.deltaTime;

        // Mantener la posici�n y original al mover el objeto
        Vector2 newPosition = Vector2.MoveTowards(new Vector2(transform.position.x, initialY),
                                                  new Vector2(target.position.x, initialY), step);
        transform.position = new Vector3(newPosition.x, initialY, transform.position.z);

        // Verificar si el objeto alcanza el objetivo (trigger)
        if (Vector2.Distance(new Vector2(transform.position.x, initialY),
                             new Vector2(target.position.x, initialY)) < 0.1f)
        {
            if (isButtonTarget)
            {
                StartCoroutine(HandleButtonInteraction());
            }
            else
            {
                StartCoroutine(DestroyTargetWithDelay()); // Iniciar corrutina para destruir el objetivo
            }
        }
        seguiraJugador = true;
    }
    private void MoveToPoint()
    {
        if (CheckCooldown1())
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane; // Ajusta la distancia del plano cercano
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            targetX = worldPosition.x; // Solo guardar la posici�n X
            isMovingToClick = true; // Iniciar el movimiento al punto de clic
            lastAttackTime = Time.time - 3f;
        }
        
    }
    private IEnumerator DestroyTargetWithDelay()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.75f); // Esperar medio segundo
        if (target != null) // Verificar si el objetivo sigue existiendo
        {
            lastAttackTime = Time.time;
            Destroy(target.gameObject); // Destruir el enemigo
            yield return new WaitForEndOfFrame();
            target = null;
        }
    }

    private IEnumerator HandleButtonInteraction()
    {
        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        lastAttackTime = Time.time;
        target = null; // Restablecer el objetivo
        objetivoFijado = false; // Terminar la interacci�n
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto colisiona con el enemigo seleccionado
        if (collision.CompareTag("Enemigo") && collision.transform == target)
        {
            StartCoroutine(DestroyTargetWithDelay()); // Iniciar corrutina para destruir el enemigo seleccionado
        }
    }

    bool CanShoot()
    {
        if (target == null) return false; // Salir si no hay objetivo

        Vector2 direction = (target.position - transform.position).normalized;
        float distance = (target.position - transform.position).magnitude;
        LayerMask layerMask = LayerMask.GetMask("Obstaculo");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, layerMask);

        // Verificamos si el rayo ha golpeado un objeto con la etiqueta "Obstaculo"
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstaculo"))
        {
            Debug.Log("Obst�culo detectado: " + hit.collider.name);
            return false; // Si hay un obst�culo, retornamos false
        }

        Debug.Log("No hay obst�culos puedo atacar");
        return true;
    }
}
