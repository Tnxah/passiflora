using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaygroundManager : MonoBehaviour
{
    public static PlaygroundManager instance;

    public GameObject obstacleSpawner;

    public FingerControl fc;

    public float speed;
    public int score;

    public bool resurrected;

    public delegate void OnDeathCallback();
    public OnDeathCallback onDeathCallback;

    private void Start()
    {
 
        if (!instance)
        {

            instance = this;

        }
        fc = GetComponent<FingerControl>();
        Initialize();
        StartCoroutine(StartGame());
        onDeathCallback += AdsManager.instance.OnDeathAds;
    }

    void Initialize()
    {
        speed = Settings.startSpeed;
    }

    IEnumerator StartGame()
    {
#if UNITY_EDITOR
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
#else
        yield return new WaitUntil(()=> FingerControl.instance.BouthTouched());
#endif
        obstacleSpawner.SetActive(true);
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
        resurrected = true;

        ObstacleSpawner.instance.Clean();

        FingerControl.instance.StopLights();

        PlaygroundUIManager.instance.OnResurrect();

        ResumeGame();
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
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
