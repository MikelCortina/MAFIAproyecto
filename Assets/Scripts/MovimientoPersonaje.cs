using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float defaultSpeed;
    public float jumpForce;
    private Rigidbody2D rb;
    public float vidaTotal = 1000f;
    public float vidaActual = 1000f;
    public PlayerShooting shooting;

    public bool facingRight = true;

    [HideInInspector] public bool isClimbing = false; // Nueva variable pública para detectar escalada

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!isClimbing) // Solo permite movimiento si NO está escalando
        {
            float moveInput = Input.GetAxis("Horizontal");

            if (!shooting.isWaitingToShoot)
            {
                rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
            }

            // Reducir velocidad al recargar
            speed = shooting.isReloading ? 1f : defaultSpeed;

            // Voltear si es necesario
            if (moveInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveInput < 0 && facingRight)
            {
                Flip();
            }

            // Salto solo si no está escalando
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBullet bullet = collision.GetComponent<EnemyBullet>();
        if (collision.CompareTag("BulletEnemy"))
        {
            vidaActual -= bullet.damage;
            Debug.Log("Has recibido un total de daño de " + bullet.damage);
            Debug.Log("Tienes un total de vida de " + vidaActual);

            Destroy(collision.gameObject);
        }
        if (vidaActual <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Has muerto");
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f).collider != null;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
