using UnityEngine;
using Unity.RemoteConfig;
using System.Threading;
using System;

public class RemoteConfig : MonoBehaviour
{
    public struct userAttributes
    {
        public bool expansionFlag;
    }

    public struct appAttributes
    {
        public int level;
        public int score;
        public string appVersion;
    }

    private float startSpeed;
    private float maxSpeed;

    private int adsCounter;

    public bool newData = false;
    public bool finished = false;

    public static RemoteConfig instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    public void Start()
    {
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        
        switch (configResponse.requestOrigin)
        {   
            case ConfigOrigin.Default:
                Debug.Log("No settings loaded this session; using default values.");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("No settings loaded this session; using cached values from a previous session.");
                startSpeed = ConfigManager.appConfig.GetFloat("StartSpeed");
                maxSpeed = ConfigManager.appConfig.GetFloat("MaxSpeed");
                adsCounter = ConfigManager.appConfig.GetInt("AdsCounter");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("New settings loaded this session; update values accordingly.");
                startSpeed = ConfigManager.appConfig.GetFloat("StartSpeed");
                maxSpeed = ConfigManager.appConfig.GetFloat("MaxSpeed");
                adsCounter = ConfigManager.appConfig.GetInt("AdsCounter");
                newData = true;
                break;
        }

        finished = true;
    }

    public float GetStartSpeed()
    {
        return startSpeed;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public int GetAdsCounter()
    {
        return adsCounter;
    }
}
