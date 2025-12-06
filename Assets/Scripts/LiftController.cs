using UnityEngine;

public class LiftController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float topY = 5f;
    float bottomY;
    Rigidbody2D rb;

    private int activeButtons = 0; // jumlah tombol yang sedang ditekan
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bottomY = rb.position.y;
    }
    void FixedUpdate()
    {
        float targetY = (activeButtons > 0) ? bottomY + topY : bottomY; // naik kalau ada tombol aktif
        Vector2 newPos = Vector2.MoveTowards(rb.position, new Vector2(rb.position.x, targetY), moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    // dipanggil oleh tombol saat ditekan
    public void ActivateButton()
    {
        activeButtons++;
    }

    // dipanggil oleh tombol saat dilepas
    public void DeactivateButton()
    {
        activeButtons = Mathf.Max(0, activeButtons - 1);
    }
}
