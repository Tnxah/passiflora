using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaygroundUIManager : MonoBehaviour
{
    public static PlaygroundUIManager instance;

    public GameObject instructionsPanel;
    public GameObject deathPanel;

    public Button resurrectButton;

    public TextMeshProUGUI[] scoreText;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        Camera.main.backgroundColor = Settings.backgroundColor;
        deathPanel.GetComponent<Image>().color = Settings.panelsColor;
    }

    public void OnRestart()
    {
        deathPanel.SetActive(false);
    }

    public void OnDeath()
    {
        deathPanel.SetActive(true);

        if (!PlaygroundManager.instance.resurrected && AdsManager.instance.RewardedIsReady())
            resurrectButton.gameObject.SetActive(true);
    }

    public void OnResurrect()
    {
        resurrectButton.gameObject.SetActive(false);
        deathPanel.SetActive(false);
    }

    public void OnPlay()
    {
        instructionsPanel.SetActive(false);
        deathPanel.SetActive(false);
    }


    private void FixedUpdate()
    {
        foreach (var text in scoreText)
        {
            text.text = PlaygroundManager.instance.score.ToString();
        }

    }
}
