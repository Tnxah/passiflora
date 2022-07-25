using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var activatable = collision.GetComponent<IActivatable>();
        if (collision.CompareTag("Obstacle"))
        {
            activatable.Activate();
        }
    }
}
