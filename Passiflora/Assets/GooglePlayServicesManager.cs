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

    public GameObject leaderboardButton;
    
    private bool _isConnectedToPlayServices;
    private bool _isConnectedToPlayServices2;

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
        PlayGamesPlatform.DebugLogEnabled = true;
        

        inited = true;
    }

    private void Start()
    {
        PlayGamesPlatform.Instance.Authenticate((success, message) => {

            if (success)
            {
                _isConnectedToPlayServices = true;
                print("Authentication success");
                DebugCustom.instance.AddDebugNote("Authentication success");
                leaderboardButton.SetActive(true);
            }
            else
            {
                _isConnectedToPlayServices = false;
                print("Authentication failed. Wot po etomu: " + message);
                DebugCustom.instance.AddDebugNote("Authentication failed. Wot po etomu: " + message);
                leaderboardButton.SetActive(false);
            }

        });

        //Social.localUser.Authenticate((bool success) => {
        //    print("Authentication: " + success);
        //    DebugCustom.instance.AddDebugNote("Authentication: " + success);
        //    _isConnectedToPlayServices2 = success;

        //    if (success)
        //    {
                
        //    }
        //    else
        //    {
                
        //    }
        //});
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            _isConnectedToPlayServices = true;
            print("Authentication success");
            DebugCustom.instance.AddDebugNote("Authentication success");
        }
        else
        {
            _isConnectedToPlayServices = false;
            print("Authentication failed. Wot po etomu: " + status.ToString());
            DebugCustom.instance.AddDebugNote("Authentication failed. Wot po etomu: " + status.ToString());
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
                DebugCustom.instance.AddDebugNote("Something went wrong in ReportScore");
            }
        });

    }

    public void ShowLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    public void DistanceAchive(int score)
    {
        Social.ReportProgress(GPGSIds.achievement_master_of_1000, score/1000, null);
    }
}
