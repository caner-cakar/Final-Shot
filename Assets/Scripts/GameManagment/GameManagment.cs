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
    private CinemachineBrain cinemachineBrain;

    private void Start()
    {
        gameEndPanel.SetActive(false);
        if (gameWinPanel != null) gameWinPanel.SetActive(false);

        if (Camera.main != null)
        {
            cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        }

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "MainMenu")
        {
            SetCursorState(true);
            Time.timeScale = 0f;
            if (menuMusicSource != null)
            {
                menuMusicSource.ignoreListenerPause = true;
                menuMusicSource.Play();
            }
        }
        else if (currentSceneName == "MainScene")
        {
            SetCursorState(false);
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
        SetCursorState(true);
        SetCinemachineBrainState(false);
        gameWon = true;
        if (gameWinPanel != null)
            gameWinPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void StartButton()
    {
        SetCursorState(false);
        if (menuMusicSource != null)
        {
            menuMusicSource.Stop();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    void PauseGame()
    {
        SetCursorState(true);
        SetCinemachineBrainState(false);

        Time.timeScale = 0f;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        
        isPaused = true;
    }

    public void ResumeGame()
    {
        SetCursorState(false);
        SetCinemachineBrainState(true);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Death()
    {
        SetCursorState(true);
        if (gameEndPanel != null)
            gameEndPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        SetCursorState(false);
        SetCinemachineBrainState(true);
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

    private void SetCursorState(bool visible)
    {
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;
    }

    private void SetCinemachineBrainState(bool enabled)
    {
        if (cinemachineBrain != null)
        {
            cinemachineBrain.enabled = enabled;
        }
    }
}
