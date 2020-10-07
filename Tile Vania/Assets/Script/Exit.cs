using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    float exitTime = 2f;
    float slowMotionTime = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel()
    {
        Time.timeScale = slowMotionTime;
        yield return new WaitForSecondsRealtime(exitTime);
        Time.timeScale = 1;
        int currentScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(currentScene);
    }
}