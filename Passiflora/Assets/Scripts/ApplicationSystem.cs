using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationSystem : MonoBehaviour
{
    public static ApplicationSystem instance;

    public delegate void Quit();
    public Quit onQuitPause;

    Dictionary<string, string> map = new Dictionary<string, string> { 
        {"Game","MainMenu"},
        {"MainMenu","Exit"} 
    };
    Dictionary<string, GameScene> gameSceneMap = new Dictionary<string, GameScene> { 
        {"MainMenu", GameScene.MainMenu},
        {"Exit", GameScene.MainMenu} 
    };


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScene(map[SceneManager.GetActiveScene().name]);
            GameManager.instance.ChangeState(gameSceneMap[map[SceneManager.GetActiveScene().name]], GameManager.gameState);
        }
    }

    void OnApplicationQuit()
    {
        onQuitPause?.Invoke();
    }

    void OnApplicationPause()
    {
        onQuitPause?.Invoke();
    }


    void LoadScene(string scene)
    {
        if (scene == "Exit")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
}
