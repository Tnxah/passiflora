using System.Collections;
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

    [SerializeField]
    private GameObject loadingAnimation;

    private float startLoadingTime;

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
        StartCoroutine(LoadPlayScene("Game"));
        loadingAnimation.SetActive(true);
    }

    public void ShowHideSettings()
    {
        Settings.LoadPlayerPrefs();
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void Init()
    {
        Debug.Log("PrefsUILoad");

        fingersOffsetToggle.isOn = Settings.isAllowedFingerOffset;
        volumeSlider.value = Settings.startVolume;
    }

    public IEnumerator LoadPlayScene(string sceneName)
    {
        startLoadingTime = Time.time;
        yield return new WaitUntil(() => Settings.IsInited() || Time.time - startLoadingTime >= 6);

        SceneManager.LoadScene(sceneName);
        GameManager.instance.ChangeState(GameScene.Game, GameState.Pause);
    }
}
