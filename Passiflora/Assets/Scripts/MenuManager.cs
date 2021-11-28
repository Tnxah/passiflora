using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel;

    public void OnFreePlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowHideSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

}
