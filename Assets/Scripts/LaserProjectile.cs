using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // Jika player punya komponen health
            PlayerController health = other.GetComponent<PlayerController>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
            Destroy(gameObject);
        
    }
}
