using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool firstBoot = true;

    public GameObject settingsPanel;

    public GameObject remoteConfigPrefab;

    private void Awake()
    {
        if (firstBoot)
        {


            firstBoot = false;
        }
    }

    public void OnFreePlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowHideSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
