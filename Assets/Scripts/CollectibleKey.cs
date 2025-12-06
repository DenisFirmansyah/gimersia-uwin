using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleKey : MonoBehaviour
{
    public GameObject closedDoor; // Pintu yang akan dibuka dengan kunci ini
    public GameObject openedDoor; // Pintu terbuka
    public AudioSource keyCollectAudio; // Suara pengambilan kunci
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // Hapus objek kunci dari scene
            Destroy(gameObject);
            // Putar suara pengambilan kunci
            if (keyCollectAudio != null)
            {
                keyCollectAudio.Play();
            }

            closedDoor.SetActive(false);
            openedDoor.SetActive(true);
        }
    }
}
