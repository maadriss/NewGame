using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    uint enemyCounter = 0;
    uint makeMoreEnemies = 10;
    uint waves = 0;
    public Enemy enemy;
    public Transform[] enemyStartPosition = new Transform[2];

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
        StartCoroutine(InstantiateEnemies());
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
    IEnumerator InstantiateEnemies()
    {
        Instantiate(enemy, enemyStartPosition[0]);
        enemyCounter++;
        yield return new WaitForSeconds(Random.Range(1, 7));
        // If not enough enemies instantiated for this wave
        if (enemyCounter < makeMoreEnemies)
        {            
            StartCoroutine(InstantiateEnemies());
        }
        // If enough enemies instantiated for this wave
        else if (enemyCounter == makeMoreEnemies)
        {
            // Add more enemies for next wave
            makeMoreEnemies += 10;
            // Make enemyCounter to 0 for next wave
            enemyCounter = 0;
            // Add on unit to wave and next wave started.
            waves++;
        }
    }
}