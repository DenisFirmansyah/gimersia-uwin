using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HpSystem : MonoBehaviour
{
    [SerializeField] private float maxHP;
    public float currentHP { get; private set; }
    private bool isDead;

    private void Awake()
    {
        currentHP = maxHP;
        isDead = false;
    }

    public void TakeDamage(float _damage)
    {
        if (isDead) return;

        currentHP = Mathf.Clamp(currentHP - _damage, 0, maxHP);

        if (currentHP > 0)
        {
            Debug.Log("Entity took " + _damage + " damage. Current HP: " + currentHP);
        }
        else
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Entity Died");
        gameObject.SetActive(false);
    }

[SerializeField] private KeyCode damageKey = KeyCode.E;

private void Update()
{
    if (Input.GetKeyDown(damageKey))
    {
        TakeDamage(1);
    }
}
}