using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private HpSystem playerHp;
    [SerializeField] private Image hpBarTotal;
    [SerializeField] private Image currentHp;

    public void Start()
    {
        hpBarTotal.fillAmount = playerHp.currentHP / 10;
    }
    public void Update()
    {
        currentHp.fillAmount = playerHp.currentHP / 10;
    }

}