using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject laserPrefab;         // Prefab laser (misalnya line laser atau projectile)
    public Transform firePoint;            // Titik keluar laser
    public float laserSpeed = 10f;         // Kecepatan laser
    public float fireInterval = 2f;        // Jeda antar tembakan
    public Direction fireDirection = Direction.Horizontal; // Arah tembakan

    private float fireTimer;

    public enum Direction
    {
        Horizontal,
        Vertical
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            FireLaser();
            fireTimer = 0f;
        }
    }

    void FireLaser()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
        
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 dir = fireDirection == Direction.Horizontal ? Vector2.right : Vector2.down;
            rb.velocity = dir * laserSpeed;
        }

        Destroy(laser, 3f); // hapus laser otomatis setelah 3 detik
    }
}
