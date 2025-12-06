using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject GameOverUI;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        GameOverUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        bool isActive = pauseMenuUI.activeSelf;
        pauseMenuUI.SetActive(!isActive);

        if (!isActive)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }

    public static void GameOver()
    {
        GameObject uiManager = GameObject.Find("UIManager");
        if (uiManager != null)
        {
            UIScript uiScript = uiManager.GetComponent<UIScript>();
            if (uiScript != null)
            {
                uiScript.ShowGameOverUI();
            }
        }
    }

    private void ShowGameOverUI()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
