using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private Toggle fingersOffsetToggle;

    [SerializeField]
    private Slider volumeSlider;


    private void Start()
    {
        fingersOffsetToggle.isOn = PlayerPrefs.GetString("FingersOffset").Equals("True");
        if(PlayerPrefs.HasKey("MusicVol"))
            volumeSlider.value = PlayerPrefs.GetFloat("MusicVol");
    }

    public void AllowOffset(bool state)
    {
        Settings.instance.allowFingerOffset = state;

        PlayerPrefs.SetString("FingersOffset", state.ToString());
    }
}
