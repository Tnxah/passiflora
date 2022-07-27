using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    public float maxSpeed;

    public float startSpeed;

    public int adsCounter;

    public bool allowFingerOffset;

    private void Awake()
    {

        if (GameObject.FindGameObjectsWithTag("Settings").Length > 1)
        {
            Destroy(gameObject);
        }

        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        StartCoroutine(CheckForUpdateSettings());
    }

    IEnumerator CheckForUpdateSettings()
    {
        yield return new WaitUntil(()=> RemoteConfig.instance.finished);
        print("settings stard getting settings data");
        startSpeed = RemoteConfig.instance.GetStartSpeed();
        maxSpeed = RemoteConfig.instance.GetMaxSpeed();
        adsCounter = RemoteConfig.instance.GetAdsCounter();


    }


}
