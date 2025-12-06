using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class FallingObject : MonoBehaviour
{
    public int damage = 3; // Damage yang diberikan ke musuh
    public LayerMask enemyLayer; // Tentukan layer musuh di Inspector
    public float minFallSpeed = 2f; // Minimal kecepatan jatuh agar dianggap sebagai serangan

    private Rigidbody2D rb;
    private bool hasHitEnemy = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah menabrak musuh
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            // Pastikan arah tabrakan dari atas
            Vector2 normal = collision.contacts[0].normal;
            bool hitFromAbove = Vector2.Dot(normal, Vector2.up) > 0.5f;

            if (hitFromAbove && rb.velocity.y <= -minFallSpeed)
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                if (enemy != null && !hasHitEnemy)
                {
                    enemy.TakeDamage(damage);
                    hasHitEnemy = true;

                    // Opsional: tambahkan efek atau hancurkan objek
                    // Destroy(gameObject);
                }
            }
        }
    }
}
