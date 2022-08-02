using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Toggle fingersOffsetToggle;

    [SerializeField]
    private Slider volumeSlider;

    [SerializeField]
    private GameObject settingsPanel;

    private void Start()
    {
        Camera.main.backgroundColor = Settings.backgroundColor;
        fingersOffsetToggle.isOn = Settings.isAllowedFingerOffset;

        if(PlayerPrefs.HasKey("MusicVol"))
            volumeSlider.value = PlayerPrefs.GetFloat("MusicVol");
    }

    public void AllowOffset(bool state)
    {
        Settings.isAllowedFingerOffset = state;

        PlayerPrefs.SetString("FingersOffset", state.ToString());
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
