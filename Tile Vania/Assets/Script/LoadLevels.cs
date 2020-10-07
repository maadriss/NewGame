using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevels : MonoBehaviour
{
    public void LoadStartLevel() { SceneManager.LoadScene(1); }
    public void LoadMenu() { SceneManager.LoadScene(0); }
    public void ExitGame() { Application.Quit(); }
}
