using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(TogglePauseMenu);
    }

    private void TogglePauseMenu()
    {
        bool isPaused = pauseMenu.activeSelf;

        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Freeze the game
        pauseMenu.SetActive(true);
        button.gameObject.SetActive(false);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Unfreeze the game
        pauseMenu.SetActive(false);
        button.gameObject.SetActive(true);
    }
}