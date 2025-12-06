using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform leftBoundary;
    public Transform rightBoundary;
    public float patrolSpeed = 2f;
    private bool movingRight = true;

    [Header("Detection Settings")]
    public float detectionRange = 5f;
    public float wallCheckDistance = 0.5f;
    public LayerMask wallLayer;
    public LayerMask playerLayer;
    public float chaseSpeed = 3.5f;

    private Rigidbody2D rb;
    private Transform playerTarget;
    private Animator animator;
    private bool isChasing = false;
    private bool isTakingHit = false;
    public int health = 3;
    [Range(1, 10)] public int damage = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isTakingHit) return;

        DetectPlayerInFront();  // ✅ deteksi player hanya di depan
        CheckWall();
        Move();
    }

    private void Move()
    {
        animator.SetBool("isMoving", true);
        float speed = isChasing ? chaseSpeed : patrolSpeed;
        float moveDir = movingRight ? 1f : -1f;
        rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);

        if (movingRight && transform.position.x >= rightBoundary.position.x)
            FlipDirection();
        else if (!movingRight && transform.position.x <= leftBoundary.position.x)
            FlipDirection();
    }

    // 🔎 Deteksi player hanya di depan musuh
    private void DetectPlayerInFront()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, playerLayer);

        if (hit.collider != null && IsWithinPatrol(hit.collider.transform.position))
        {
            if (hit.collider.CompareTag("Player1") || hit.collider.CompareTag("Player2"))
            {
                isChasing = true;
                playerTarget = hit.collider.transform;
                return;
            }
        }

        isChasing = false; // Tidak ada player di depan
    }

    private void CheckWall()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, direction, wallCheckDistance, wallLayer);

        if (wallHit.collider != null)
        {
            FlipDirection();
        }
    }

    private bool IsWithinPatrol(Vector2 pos)
    {
        return pos.x >= leftBoundary.position.x && pos.x <= rightBoundary.position.x;
    }

    private void FlipDirection()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Serang player
        if (collision.collider.CompareTag("Player1") || collision.collider.CompareTag("Player2"))
        {
            PlayerController player = collision.collider.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 dir = movingRight ? Vector3.right : Vector3.left;
        Gizmos.DrawLine(transform.position, transform.position + dir * detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + dir * wallCheckDistance);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Die");
        // Bisa tambahkan animasi, efek partikel, dsb
        Destroy(gameObject);
    }
}
