using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] InputAction MoveAction;
    [SerializeField] InputAction JumpButton;

    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 500f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    [Header("Health Settings")]
    public int maxHP = 5;
    public int currentHP;
    public float invincibilityDuration = 1f; // waktu kebal setelah kena jebakan
    private bool isInvincible = false;

    [Header("Audio Settings")]
    public AudioSource damageAudioSource;
    public AudioSource jumpAudioSource;
    public AudioSource walkingAudioSource;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 move; 
    private bool isGrounded;

    private SpriteRenderer spriteRenderer; // efek visual (kedip)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        MoveAction.Enable();
        JumpButton.Enable();
        JumpButton.performed += ctx => TryJump();

        currentHP = maxHP; // inisialisasi HP penuh di awal
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        move = MoveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(move.x * movementSpeed, rb.velocity.y);
        Vector3 scale = transform.localScale;
        
        if (move.x > 0)
            scale.x = -Mathf.Abs(scale.x);
        else if (move.x < 0)
            scale.x = Mathf.Abs(scale.x);

        if (move.x != 0 && isGrounded){
            walkingAudioSource.Play();
            animator.SetBool("isWalking", true);
        }
        else {
            walkingAudioSource.Stop();
            animator.SetBool("isWalking", false);
        }

        animator.SetBool("isJumping", isGrounded == false && rb.velocity.y > 0);
        animator.SetBool("isFalling", !isGrounded && rb.velocity.y <= 0);

        transform.localScale = scale;
    }

    private void TryJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpAudioSource.Play();
        }
    }

    // 🩸 Dipanggil saat player menerima damage
    public void TakeDamage(int damage)
    {
        if (isInvincible) return; // tidak bisa kena damage selama iFrame

        damageAudioSource.Play();
        // pastikan damage minimal 1 dan maksimal maxHP
        int finalDamage = Mathf.Clamp(damage, 1, maxHP);

        currentHP -= finalDamage;
        currentHP = Mathf.Max(currentHP, 0); // jangan kurang dari 0

        Debug.Log($"Player terkena damage {finalDamage}, sisa HP: {currentHP}");
        animator.SetTrigger("hit");

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityFrame());
        }
    }

    // 🕹️ Coroutine untuk iFrame
    private IEnumerator InvincibilityFrame()
    {
        isInvincible = true;

        // efek visual — player berkedip selama kebal
        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("Player mati!");
        animator.SetTrigger("die");
        rb.velocity = Vector2.zero;
        this.enabled = false; // nonaktifkan kontrol player
        StartCoroutine(HandleGameOver());
    }

    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(2f); // tunggu animasi mati selesai
        UIScript.GameOver();
    }
}
