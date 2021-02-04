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
    uint makeMoreEnemies = 1;
    uint waves = 1;
    public Text waveText;
    public Enemy enemy;
    public Transform[] enemyStartPosition = new Transform[2];
    // The player in the scene.
    GameObject player;    
    

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
        
    }

    void Start()
    {
        waveText.text = waves.ToString();
        player = GameObject.Find("Player");
        enemyStartPosition[0] = GameObject.Find("EnemyStartPosition1").transform;
        enemyStartPosition[1] = GameObject.Find("EnemyStartPosition2").transform;
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
        else 
        {
            //ResetGameSession();            
            // #Bug: If player start from firs point of game and then an enemy stay there this means player dies again.
        }
    }

    
    private void TakeLife()
    {
        // Decrease player health, when player dies start again from startPos position.
        playerLives--;                
        /*
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        */
        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void StartNextWave()
    {
        // If last enemy dies then go to next wave.        
        if (enemy.enemyDies && enemyCounter == makeMoreEnemies)
        {
            // Add more enemies for next wave
            makeMoreEnemies += 10;
            // Make enemyCounter to 0 for next wave
            enemyCounter = 0;
            // Add on unit to wave and next wave started.
            waves++;
            waveText.text = waves.ToString();
            // Show the the next wave text animation.
            // Go to next wave
        }
    }

    IEnumerator InstantiateEnemies()
    {
        Instantiate(enemy, enemyStartPosition[Random.Range(0, 2)]);
        enemyCounter++;
        yield return new WaitForSeconds(Random.Range(1, 7));
        // If not enough enemies instantiated for this wave
        if (enemyCounter < makeMoreEnemies)
        {            
            StartCoroutine(InstantiateEnemies());
        }
        // If enough enemies instantiated for this wave        
    }
}