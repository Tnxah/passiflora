using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaygroundManager : MonoBehaviour
{
    public static PlaygroundManager instance;

    public ObstacleSpawner obstacleSpawner;

    public FingerControl fc;

    public float speed;
    public int score;

    public bool resurrected;

    public delegate void OnDeathCallback();
    public OnDeathCallback onDeathCallback;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        
    }

    private void Start()
    {
        obstacleSpawner = GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>();
        fc = GetComponent<FingerControl>();
        Initialize();
        StartCoroutine(StartGame());
    }

    void Initialize()
    {
        speed = Settings.startSpeed;
    }

    IEnumerator StartGame()
    {
        Time.timeScale = 1;

#if UNITY_EDITOR
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
#else
        yield return new WaitUntil(()=> FingerControl.instance.BouthTouched());
#endif
        GameManager.instance.ChangeState(GameManager.gameScene, GameState.Play);

        PlaygroundUIManager.instance.OnPlay();
        obstacleSpawner.Launch();
        StartCoroutine(ScoreIncreaser());
        StartCoroutine(SpeedIncreaser());
    }

    IEnumerator ScoreIncreaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / (speed / 10));
            ObstacleSpawner.instance.FillPool(score);
            score++;
        }
        
    }

    IEnumerator SpeedIncreaser()
    {
        while (true)
        {
            while (speed < Settings.maxSpeed)
            {
                yield return new WaitForSeconds(10);
                speed++;
            }
            
        }
    }
    public void OnDeath()
    {
        PauseGame();

        PlaygroundUIManager.instance.OnDeath();

        onDeathCallback?.Invoke();

        GooglePlayServicesManager.instance.SaveScore(score);
        GooglePlayServicesManager.instance.DistanceAchive(score);
    }

    public void Resurrect()
    {
        ResumeGame();
        resurrected = true;

        ObstacleSpawner.instance.Clean();

        FingerControl.instance.StopLights();

        PlaygroundUIManager.instance.OnResurrect();

        
    }

    public void Restart()
    {
        ResumeGame();
        PlaygroundUIManager.instance.OnRestart();
        resurrected = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        GameManager.instance.ChangeState(GameManager.gameScene, GameState.Pause);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
