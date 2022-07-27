using System;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
#if UNITY_ANDROID
    string gameID = "4473937";
    string interstitialId = "Interstitial_Android";
    string rewardedID = "Rewarded_Android";
    string bannerID = "Banner_Android";
#else
    string gameId = "4473936";
    string interstitialId = "Interstitial_iOS";
    string rewardedId = "Rewarded_iOS";
    string bannerId = "Banner_iOS";
#endif 

    public TextMeshProUGUI debug;
    //[HideInInspector]
    public int numberToAd;
    //[SerializeField]
    public static int countToAd = 2;
    Action onRewardedAdSuccess;
    public static AdsManager instance;

    private bool rewardedReady;

    private void Awake()
    {
#if UNITY_EDITOR
        //return;
#endif
        print("start Initialising");
        Advertisement.Initialize(gameID, false, this);
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        LoadBanner();
        LoadInterstitial();
        LoadRewarded();
        ShowBanner();
        numberToAd = Settings.instance.adsCounter;
    }

    private void LoadInterstitial()
    {
        Advertisement.Load(interstitialId, this);
    }

    private void LoadRewarded()
    {
        Advertisement.Load(rewardedID, this);
    }

    public void ShowInterstitial()
    {
        Advertisement.Show(interstitialId, this);
    }

    public void ShowRewarded()
    {
        Advertisement.Show(rewardedID, this);
    }

    public bool RewardedIsReady()
    {
        return rewardedReady;
    }

    public void ShowBanner()
    {
        debug.text += "show banner";

        
        Advertisement.Banner.Show(bannerID);
    }


    public void HideBanner()
    {
        Advertisement.Banner.Hide(false);
    }

    public void LoadBanner()
    {
        debug.text += "Banner start loading";
        print("Banner start loading");
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(bannerID);
        
    }

    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        debug.text += "Banner loaded";
        ShowBanner();

    }


    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        debug.text += $"Banner Error: {message}";


    }

    public void OnDeathAds()
    {
        countToAd--;
        if (countToAd <= 0)
        {
            ShowInterstitial();
            countToAd = numberToAd;
        }
    }


    public void OnUnityAdsReady(string placementId)
    {
        print("ADS ARE READY");

    }

    public void OnUnityAdsDidError(string message)
    {
        print("ERROR " + message);
        debug.text += ("ERROR " + message);

    }

    public void OnUnityAdsDidStart(string placementId)
    {
        print("VIDEO STARTED");

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardedID && showResult == ShowResult.Finished)
        {
            print("REWARD");
            //***********
            // GIVE REWARD TO THE PLAYER
            onRewardedAdSuccess.Invoke();
            //***********
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        debug.text += "Kek";
        debug.text += "Unity Ads initialization complete.";
        Debug.Log("======================================");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        debug.text += $"Unity Ads Initialization Failed: {error.ToString()} - {message}";
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print(placementId + " starded showing");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        print("plus babki");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(rewardedID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            GameManager.instance.Resurrect();

            rewardedReady = false;
            // Load another ad:
            Advertisement.Load(rewardedID, this);
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(rewardedID))
        {
            print("Rewarded loaded na pewno");
            rewardedReady = true;
        }

        print(placementId + " loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new NotImplementedException();
    }
}
