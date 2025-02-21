using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed = 5f;
    private Rigidbody2D playerRb;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRb = collision.GetComponent<Rigidbody2D>();
            playerRb.gravityScale = 0f;

            float verticalInput = Input.GetAxisRaw("Vertical");
            playerRb.linearVelocity = new Vector2(0f, verticalInput * climbSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRb = collision.GetComponent<Rigidbody2D>();
            playerRb.gravityScale = 1f;
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);
        }
    }
}
