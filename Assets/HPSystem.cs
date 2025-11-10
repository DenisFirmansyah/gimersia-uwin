using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HPsystem : MonoBehaviour
{
    [SerializeField] private float maxHP;
    private float currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }
    public void TakeDamage(float _damage)
    {
        currentHP = Mathf.Clamp(currentHP - _damage, 0, maxHP);

        if (currentHP > 0)
        {
            Debug.Log("Entity took " + _damage + " damage. Current HP: " + currentHP);
        }
        else
        {
            Debug.Log("Entity Died");
        }
    }
}