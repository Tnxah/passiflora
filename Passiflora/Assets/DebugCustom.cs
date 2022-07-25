using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCustom : MonoBehaviour
{
    public static DebugCustom instance;
    public TextMeshProUGUI debugText;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void AddDebugNote(string text)
    {
        debugText.text += (text + "\n"); 
    }
}
