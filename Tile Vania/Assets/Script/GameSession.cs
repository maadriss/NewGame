using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    private void Awake()
    {           
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }       
    }
    
    void Start()
    {        
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "SuccessMenu")
        {
            Destroy(gameObject);
        }
    }
    public void AddToScore(int pointToScore)
    {
        score += pointToScore;
        scoreText.text = score.ToString();
    }
    
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1) { TakeLife(); }
        else { ResetGameSession(); }
    }

    
    private void TakeLife()
    {
        // Decrease player health, when player dies start again from startPos position.
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);        
        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}