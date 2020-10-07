using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameSession;
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void Menu()
    {        
        SceneManager.LoadScene(0);
        if (GameObject.Find("GameSession"))
        {
            gameSession = GameObject.FindObjectOfType<GameSession>().gameObject;
            Destroy(gameSession);
        }
        pausePanel.SetActive(false);        
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}