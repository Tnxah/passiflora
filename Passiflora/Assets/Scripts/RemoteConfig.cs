using UnityEngine;
using Unity.RemoteConfig;
using System.Threading;

public class RemoteConfig : MonoBehaviour
{
    public struct userAttributes
    {
        // Optionally declare variables for any custom user attributes:
        public bool expansionFlag;
    }

    public struct appAttributes
    {
        // Optionally declare variables for any custom app attributes:
        public int level;
        public int score;
        public string appVersion;
    }

    // Optionally declare a unique assignmentId if you need it for tracking:
    public string assignmentId;

    // Declare any Settings variables you’ll want to configure remotely:
    public int enemyVolume;
    public float enemyHealth;
    public float enemyDamage;


    public float startSpeed;
    public float maxSpeed;

    public int adsCounter;


    public bool newData = false;
    public bool finished = false;

    public static RemoteConfig instance;
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        if (GameObject.FindGameObjectsWithTag("RemoteConfig").Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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
