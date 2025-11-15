using System.Collections;
using UnityEngine;

public class SpikeTrapController : MonoBehaviour
{
    [Header("Collider Settings")]
    [SerializeField] private Collider2D blockCollider;
    [SerializeField] private Collider2D damageCollider;

    [Header("Damage Settings")]
    [SerializeField] private float damage = 1f;
    [SerializeField] private bool continuousDamage = false;
    [SerializeField] private float damageInterval = 0.5f;

    private Coroutine damageRoutine;

    private void Reset()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        if (colliders.Length >= 2)
        {
            blockCollider = colliders[0];
            damageCollider = colliders[1];
        }
    }

    private void Awake()
    {
        SetCollidersActive(false);
    }

    private void SetCollidersActive(bool active)
    {
        if (blockCollider) blockCollider.enabled = active;
        if (damageCollider) damageCollider.enabled = active;
    }

    public void EnableHit()
    {
        SetCollidersActive(true);
    }

    public void DisableHit()
    {
        SetCollidersActive(false);
        if (damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
            damageRoutine = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!damageCollider || other != damageCollider) return;

        int layer = other.gameObject.layer;
        if (layer != LayerMask.NameToLayer("Player1") &&
            layer != LayerMask.NameToLayer("Player2"))
            return;

        HpSystem hp = other.GetComponent<HpSystem>();
        if (hp == null)
            hp = other.GetComponentInParent<HpSystem>();

        if (hp == null) return;

        if (continuousDamage)
        {
            damageRoutine = StartCoroutine(DamageRoutine(hp));
        }
        else
        {
            hp.TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (continuousDamage && damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
            damageRoutine = null;
        }
    }

    private IEnumerator DamageRoutine(HpSystem hp)
    {
        while (true)
        {
            if (hp == null) yield break;
            hp.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}