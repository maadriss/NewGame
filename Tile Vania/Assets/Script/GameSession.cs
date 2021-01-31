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
    byte enemyCounter = 0;
    //Give student buttons
    //
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
    //  :|
    IEnumerator InstantiateEnemies()
    {
        Instantiate(enemy, enemyStartPosition[0]);
        enemyCounter++;
        yield return new WaitForSeconds(1f);
        if (enemyCounter < 10)
        {
            StartCoroutine(InstantiateEnemies());            
        }
    }
}