using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public HPBarSys hpbarsys;
    private void Start()
    {
 HPSystem hpsystem = new HPSystem(100);
        hpbarsys.Setup(hpsystem);
        hpsystem.DMG(30);
        Debug.Log("HP - 30: " + hpsystem.GetHP());
        Debug.Log("Ratio: " + hpsystem.GetHPRatio());
    }
}
