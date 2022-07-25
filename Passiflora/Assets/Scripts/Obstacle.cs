using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        GetComponent<Animator>().Play("Wake");
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
        //transform.RotateAround(Vector3.zero, Vector3.left, 1f);
    }
}
