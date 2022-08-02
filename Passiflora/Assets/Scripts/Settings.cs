using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings
{
    public static float maxSpeed;
    public static float startSpeed;
    public static float startVolume = 80;

    public static int adsCounter;

    public static bool isAllowedFingerOffset;
    public static bool isRemoteConfigLoaded;
    public static bool isPlayerPrefsLoaded;
    public static bool isConnectedToPlayServices;
    public static bool inited;

    public static Color backgroundColor;
    public static Color playerColor;
    public static List<Color> obstacleColor;
    public static Color panelsColor;

    public delegate void OnInit();
    public static OnInit onInitCallback;

    public delegate void OnPlayerPrefs();
    public static OnPlayerPrefs onPlayerPrefsCallback;

    public delegate void OnRemoteConfig();
    public static OnRemoteConfig onRemoteConfigCallback;


    public static void LoadRemoteConfig()
    {
        startSpeed = RemoteConfig.instance.GetStartSpeed();
        maxSpeed = RemoteConfig.instance.GetMaxSpeed();
        adsCounter = RemoteConfig.instance.GetAdsCounter();

        Debug.Log("speed " + startSpeed + "mspeed " + maxSpeed + "acounter " + adsCounter);

        onRemoteConfigCallback?.Invoke();
        isRemoteConfigLoaded = true;
    }

    public static void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("MusicVol"))
            startVolume = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("FingersOffset"))
            isAllowedFingerOffset = PlayerPrefs.GetString("FingersOffset").Equals("True");

        onPlayerPrefsCallback?.Invoke();
        isPlayerPrefsLoaded = true;
    }
}
