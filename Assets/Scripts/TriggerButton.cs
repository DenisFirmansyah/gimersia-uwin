using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{
    public GameObject door; // Assign the door GameObject in the Inspector
    private Animator doorAnimator;

    void Start()
    {
        if (door != null)
        {
            doorAnimator = door.GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ketika player menginjak tombol → buka trapdoor
        if (other.CompareTag("Player1") || other.CompareTag("Player2") && doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", true);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.2f, transform.localPosition.z);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Ketika player keluar dari area tombol → tutup trapdoor
        if (other.CompareTag("Player1") || other.CompareTag("Player2") && doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", false);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.2f, transform.localPosition.z);
        }
    }
}
