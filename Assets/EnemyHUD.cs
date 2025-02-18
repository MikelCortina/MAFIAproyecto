using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public Image alertImage;  // Imagen para el estado de alerta
    public Image attackImage; // Imagen para el estado de ataque

    private EnemyFSM fsm;     // Referencia al script EnemyFSM
    private EnemyFSM.EnemyState lastState; // �ltimo estado conocido de la FSM

    private void Start()
    {
        // Inicializar HUD y obtener referencia a EnemyFSM
        InitializeHUD();
        fsm = GetComponent<EnemyFSM>();
        if (fsm == null)
        {
            Debug.LogError("EnemyFSM no est� asignado en EnemyHUD.");
            return;
        }

        // Configurar el HUD seg�n el estado inicial de la FSM
        UpdateHUD(fsm.currentState);
        lastState = fsm.currentState;
    }

    private void Update()
    {
        if (fsm == null) return;

        // Detectar cambio de estado en la FSM
        if (fsm.currentState != lastState)
        {
            UpdateHUD(fsm.currentState);
            lastState = fsm.currentState; // Actualizar el �ltimo estado conocido
        }
    }

    /// <summary>
    /// Inicializa el HUD desactivando todas las im�genes.
    /// </summary>
    public void InitializeHUD()
    {
        if (alertImage != null) alertImage.gameObject.SetActive(false);
        if (attackImage != null) attackImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// Actualiza el HUD en funci�n del estado del enemigo.
    /// </summary>
    /// <param name="state">Estado actual del enemigo.</param>
    public void UpdateHUD(EnemyFSM.EnemyState state)
    {
        // Activar/desactivar im�genes seg�n el estado
        if (alertImage != null) alertImage.gameObject.SetActive(state == EnemyFSM.EnemyState.Alert);
        if (attackImage != null) attackImage.gameObject.SetActive(state == EnemyFSM.EnemyState.Attack);
    }
}
