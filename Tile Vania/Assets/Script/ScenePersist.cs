using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{    
    int start_scene_index;    
    
    private void Awake()
    {        
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
        if (current_scene_index != start_scene_index)
        {
            
            Destroy(gameObject);
        }
    }
}