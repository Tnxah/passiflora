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

    private void Awake()
    {
        Settings.onPlayerPrefsCallback += Init;
    }

    private void Start()
    {
        settingsPanel.GetComponent<Image>().color = Settings.backgroundColor;
        Camera.main.backgroundColor = Settings.backgroundColor;
    }
    public void AllowOffset(bool state)
    {
        Settings.isAllowedFingerOffset = state;

        PlayerPrefs.SetString("FingersOffset", state.ToString());
    }

    public void OnFreePlayButton()
    {
        SceneManager.LoadScene("Game");
        GameManager.instance.ChangeState(GameScene.Game, GameState.Pause);
    }

    public void ShowHideSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void Init()
    {
        Debug.Log("PrefsUILoad");

        fingersOffsetToggle.isOn = Settings.isAllowedFingerOffset;
        volumeSlider.value = Settings.startVolume;
    }
}
