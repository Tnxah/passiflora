using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GooglePlayServicesManager : MonoBehaviour
{
    public static GooglePlayServicesManager instance;
    [HideInInspector]
    public bool inited;
    
    private bool _isConnectedToPlayServices;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            Init();
        }
    }

    public void Init()
    {
        if (inited)
            return;

        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);

        inited = true;
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            _isConnectedToPlayServices = true;
        }
        else
        {
            _isConnectedToPlayServices = false;
        }
    }

    public void SaveScore(int score)
    {
        if (!_isConnectedToPlayServices)
            return;

        Social.ReportScore(score, GPGSIds.leaderboard_leaderboard, (success) =>
        {
            if (!success)
            {
                print("something went wrong");
            }
        });

    }
}
