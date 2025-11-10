using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HPBarSys : MonoBehaviour
{
    private HPSystem hpsystem;
    public void Setup(HPSystem hpsystem)
    {
        this.hpsystem = hpsystem;
        hpsystem.OnHPChanged += HPSystem_OnHPChanged;
    }
    private void Update()
    {
    }

    private void HPSystem_OnHPChanged(object sender, EventArgs e)
    {
        transform.Find("hpBar").localScale = new Vector3(hpsystem.GetHPRatio(), 1);
    }
}