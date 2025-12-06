using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Jika pakai TextMeshPro

public class DoorInteraction : MonoBehaviour
{
    public string nextSceneName;            // Nama scene tujuan
    public GameObject pressFTextPrefab;     // Prefab teks “Press F to Enter”
    private GameObject player1UI, player2UI;

    public AudioSource doorAudioSource;    // Suara pintu
    private bool player1Inside = false;
    private bool player2Inside = false;
    private bool player1Entered = false;
    private bool player2Entered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1Inside = true;
            ShowPressFUI(other.gameObject, ref player1UI);
        }
        else if (other.CompareTag("Player2"))
        {
            player2Inside = true;
            ShowPressFUI(other.gameObject, ref player2UI);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1Inside = false;
            HidePressFUI(ref player1UI);
        }
        else if (other.CompareTag("Player2"))
        {
            player2Inside = false;
            HidePressFUI(ref player2UI);
        }
    }

    void Update()
    {
        // Player 1
        if (player1Inside && !player1Entered && Input.GetKeyDown(KeyCode.F))
        {
            GameObject p1 = GameObject.FindGameObjectWithTag("Player1");
            StartCoroutine(EnterDoor(p1, 1));
        }

        // Player 2
        if (player2Inside && !player2Entered && Input.GetKeyDown(KeyCode.F))
        {
            GameObject p2 = GameObject.FindGameObjectWithTag("Player2");
            StartCoroutine(EnterDoor(p2, 2));
        }
    }

    private IEnumerator EnterDoor(GameObject player, int playerNum)
    {
        // Sembunyikan UI
        if (playerNum == 1) HidePressFUI(ref player1UI);
        else HidePressFUI(ref player2UI);

        // Nonaktifkan player
        player.SetActive(false);
        // Putar suara pintu
        if (doorAudioSource != null)
        {
            doorAudioSource.Play();
        }

        if (playerNum == 1) player1Entered = true;
        else player2Entered = true;

        // Cek jika kedua player sudah masuk
        if (player1Entered && player2Entered)
        {
            yield return new WaitForSeconds(1f); // delay kecil
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void ShowPressFUI(GameObject player, ref GameObject uiInstance)
    {
        if (pressFTextPrefab == null) return;

        // Posisi muncul di atas kepala player
        Vector3 uiPosition = player.transform.position + Vector3.up * 1.5f;

        uiInstance = Instantiate(pressFTextPrefab, uiPosition, Quaternion.identity);
        uiInstance.transform.SetParent(player.transform); // UI mengikuti posisi player
    }

    private void HidePressFUI(ref GameObject uiInstance)
    {
        if (uiInstance != null)
        {
            Destroy(uiInstance);
            uiInstance = null;
        }
    }
}
