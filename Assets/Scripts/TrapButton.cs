using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour
{
    public GameObject trap;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (trap != null)
            {
                if (trap.activeSelf)
                {
                    trap.SetActive(false);
                }
                else
                {
                    trap.SetActive(true);
                }
            }
        }
    }
}
