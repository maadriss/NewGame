using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    //if awakeScene != currentScene

    int start_scene_index;
    int awakeScene;
    
    private void Awake()
    {
        awakeScene = SceneManager.GetActiveScene().buildIndex;
        int number_of_persist = FindObjectsOfType<ScenePersist>().Length;
        if (number_of_persist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        start_scene_index = SceneManager.GetActiveScene().buildIndex;        
    }    

    void Update()
    {        
        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Update: " + current_scene_index);
        if (current_scene_index != start_scene_index)
        {
            
            Destroy(gameObject);
        }
    }
}