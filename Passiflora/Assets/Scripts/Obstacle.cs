using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    private System.Random rnd;
    private int rotation;

    private void Start()
    {
        rnd = new System.Random();
        rotation = rnd.Next(-50, 51);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.OnDeath();
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotation * Time.fixedDeltaTime);
    }
}
