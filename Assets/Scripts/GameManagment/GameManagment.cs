using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManagment : MonoBehaviour
{
    public GameObject gameEndPanel;
    public GameObject gameWinPanel;
    public AudioSource menuMusicSource;
    public GameObject pauseMenuPanel;
    public CinemachineVirtualCamera virtualCam;


    private bool isPaused = false;
    private bool gameWon = false;
    private float checkInterval = 0.5f;
    private float nextCheckTime = 0f;

    private void Start()
    {
        gameEndPanel.SetActive(false);
        if (gameWinPanel != null) gameWinPanel.SetActive(false);

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "MainMenu")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            if (menuMusicSource != null)
            {
                menuMusicSource.ignoreListenerPause = true;
                menuMusicSource.Play();
            }
        }
        else if (currentSceneName == "MainScene")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainScene") return;

        if (Time.time >= nextCheckTime && !gameWon)
        {
            nextCheckTime = Time.time + checkInterval;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length == 0)
            {
                GameWin();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameEndPanel != null && gameEndPanel.activeInHierarchy) return;

            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    void GameWin()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Camera.main.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        gameWon = true;
        if (gameWinPanel != null)
            gameWinPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void StartButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (menuMusicSource != null)
        {
            menuMusicSource.Stop();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
        Camera.main.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;

        Time.timeScale = 0f;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera.main.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Death()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (gameEndPanel != null)
            gameEndPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera.main.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
