using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText
        ;

    public float speed = 150;
    public int score = 0;


    private void Start()
    {
        if (!instance)
        {
            instance = this;
        }

        StartCoroutine(ScoreIncreaser());
    }

    private void FixedUpdate()
    {
        scoreText.text = score.ToString();
    }

    IEnumerator ScoreIncreaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / (speed / 10));
            score++;
        }
        
    }
}
