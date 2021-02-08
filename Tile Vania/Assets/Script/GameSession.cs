using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameSession : MonoBehaviour
{


    //_Bugs:
    // 1- Health decrease two times
    // 2- Second way doesn't starts.
    // 3- Player doesn't die because after two points decrease from life then
    // one point remains and never this one point decreases.

    // Things to do now:
    // Each wave starts after previes wave.
    // Each wave more enemies instantiated
    // Each wave powerful enemies instantiated
    // Randomly enemies instantiated with every power.
    // Add health for every enemy.

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject[] enemyKind = new GameObject[6];
    uint enemyCounter = 0;
    uint makeMoreEnemies = 10;
    uint waves = 1;
    public Text waveText;
    public Enemy enemy;
    public Transform[] enemyStartPosition = new Transform[2];
    GameObject player_shield;
    // The player in the scene.
    GameObject player;
    [SerializeField] uint enemies_number;
    // Get enemies number for other classes   
    public void CountEnemies()
    {
        enemies_number++;
    }

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
        
    }

    void Start()
    {
        Find_Game_Objects();
        waveText.text = waves.ToString();        
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
        StartCoroutine(InstantiateEnemies());        
    }

    // For searching all game objects we want.
    void Find_Game_Objects()
    {
        player_shield = GameObject.Find("PlayerShield");
        player = GameObject.Find("Player");
        for (int i = 0; i < enemyStartPosition.Length; i++)
        {            
            enemyStartPosition[i] = GameObject.Find("EnemyStartPosition" + (1 + i)).transform;
        }                
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
            // show Game over Panel 

            //ResetGameSession();            
            // _Bug: If player start from firs point of game and then an enemy stay there this means player dies again.
        }
    }

    
    private void TakeLife()
    {
        // Decrease player health,         
        playerLives--;
        // when player dies start again from startPos position.
        player.transform.position = Player._player.start_position.transform.position;

        // PlayerShield activated


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
        Instantiate(enemyKind[Random.Range(0, 6)], enemyStartPosition[Random.Range(0, 2)]);
        enemyCounter++;
        yield return new WaitForSeconds(Random.Range(1, 5));
        // If not enough enemies instantiated for this wave
        if (enemyCounter < makeMoreEnemies)
        {            
            StartCoroutine(InstantiateEnemies());
        }
        // If enough enemies instantiated for this wave        
    }
}