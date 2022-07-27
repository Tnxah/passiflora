using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI[] scoreText;
    public GameObject deathPanel;

    public GameObject obstacleSpawner;
    public GameObject instructionsPanel;

    public Button resurrectButton;

    public FingerControl fc;

    public float maxSpeed;

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
        speed = Settings.instance.startSpeed;
        maxSpeed = Settings.instance.maxSpeed;
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

        instructionsPanel.SetActive(false);
    }


    private void FixedUpdate()
    {
        foreach (var text in scoreText)
        {
            text.text = score.ToString();
        }
        
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
            while (speed < maxSpeed)
            {
                yield return new WaitForSeconds(10);
                speed++;
            }
            
        }
    }
    public void OnDeath()
    {
        PauseGame();

        deathPanel.SetActive(true);

        onDeathCallback?.Invoke();

        GooglePlayServicesManager.instance.SaveScore(score);
        GooglePlayServicesManager.instance.DistanceAchive(score);

        if (!resurrected && AdsManager.instance.RewardedIsReady())
        resurrectButton.gameObject.SetActive(true);
    }

    public void Resurrect()
    {
        resurrected = true;

        ObstacleSpawner.instance.Clean();

        resurrectButton.gameObject.SetActive(false);

        deathPanel.SetActive(false);

        FingerControl.instance.StopLights();
        
        ResumeGame();
    }

    public void Restart()
    {
        ResumeGame();
        deathPanel.SetActive(false);
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
