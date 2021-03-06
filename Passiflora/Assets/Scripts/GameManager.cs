using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI[] scoreText;
    public GameObject deathPanel;

    public GameObject obstacleSpawner;
    public GameObject instructionsPanel;

    float maxSpeed = 650;

    public float speed = 150;
    public int score = 0;


    private void Start()
    {
            
        if (!instance)
        {
            
            instance = this;
            
        }
        Initialize();
        StartCoroutine(StartGame());
        
    }

    void Initialize()
    {
        speed = Settings.instance.startSpeed;
        maxSpeed = Settings.instance.maxSpeed;
    }

    IEnumerator StartGame()
    {
        
        yield return new WaitUntil(()=> FingerControl.instance.BouthTouched());

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
            score++;
        }
        
    }

    IEnumerator SpeedIncreaser()
    {
        
            while (speed < maxSpeed)
            {
                yield return new WaitForSeconds(5);
                speed++;
            }
         
    }
    public void OnDeath()
    {
        PauseGame();
        deathPanel.SetActive(true);
        AdsManager.instance.countToAd--;
        if (AdsManager.instance.countToAd == 0)
        {
            AdsManager.instance.ShowInterstitial();
            AdsManager.instance.countToAd = AdsManager.instance.numberToAd;
        }
    }

    public void Restart()
    {
        ResumeGame();
        deathPanel.SetActive(false);
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
