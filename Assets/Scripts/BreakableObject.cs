using System.Collections;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private bool isBreaking = false;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player1") ||collision.collider.CompareTag("Player2") && !isBreaking)
        {
            StartCoroutine(BreakBlock());
        }
    }

    IEnumerator BreakBlock()
    {
        isBreaking = true; // mencegah pemanggilan ganda
        yield return new WaitForSeconds(2f);

        boxCollider.enabled = false; // nonaktifkan collider
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.6f); // transparansi 50%

        yield return new WaitForSeconds(2f);
        isBreaking = false;

        boxCollider.enabled = true; // aktifkan kembali collider
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // kembalikan ke opak
    }
}
