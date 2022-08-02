using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool firstBoot = true;

    public Color backgroundColor;

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
        #if UNITY_EDITOR
            Settings.backgroundColor = this.backgroundColor;
        #endif

        Settings.LoadPlayerPrefs();
    }
}
