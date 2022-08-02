using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool firstBoot = true;

    public RemoteConfig remoteConfig;

    private void Awake()
    {
        if (firstBoot)
        {
            FirstBoot();

            firstBoot = false;
        }
    }

    private void FirstBoot()
    {
        remoteConfig.enabled = true;

        Settings.LoadPlayerPrefs();
        StartCoroutine(LoadRemoteConfig());
    }

    private IEnumerator LoadRemoteConfig()
    {
        yield return new WaitUntil(() => RemoteConfig.instance.finished);
        Settings.LoadRemoteConfig();
    }
}
