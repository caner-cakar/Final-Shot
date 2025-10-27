using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManagment : MonoBehaviour
{
    public GameObject gameEndPanel;

    public AudioSource menuMusicSource;    

    public GameObject pauseMenuPanel;
    private bool isPaused = false;

    private void Start()
    {
        gameEndPanel.SetActive(false);
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "MainMenu")
        {
            Time.timeScale = 0f; 
            if (menuMusicSource != null)
            {
                menuMusicSource.ignoreListenerPause = true;
                menuMusicSource.Play();
            }
        }
        else if (currentSceneName == "MainScene")
        {
            Time.timeScale = 1f; 
            if (gameEndPanel != null)
            {
                gameEndPanel.SetActive(false);
            }
        }
    }
    public void StartButton()
    {
        if (menuMusicSource != null)
        {
            menuMusicSource.Stop(); 
        }
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Eğer "Ölüm Paneli" açıksa, Pause menüsünü açma
            if (gameEndPanel != null && gameEndPanel.activeInHierarchy)
            {
                return;
            }

            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Death()
    {
      
        if (gameEndPanel != null)
        {
            gameEndPanel.SetActive(true);
        }
        Time.timeScale = 0f;
        
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
