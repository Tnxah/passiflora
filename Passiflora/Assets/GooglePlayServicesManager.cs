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

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (inited)
            return;

        instance = this;

        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;

        Authenticate();

        inited = true;
    }



    private void Authenticate()
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
