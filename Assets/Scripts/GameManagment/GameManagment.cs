using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManagment : MonoBehaviour
{
    public GameObject gameEndPanel;

    private void Start()
    {
        gameEndPanel.SetActive(false);
    }
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Death();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameEndPanel.SetActive(false);
        }
    }

    public void Death()//karakter öldüðünde çaðýrýlacak fonksiyon;
    {
      
        gameEndPanel.SetActive(true);
        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
