using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obst;

    float width;
    void Start()
    {
        width = GetComponent<BoxCollider2D>().size.x;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject obs = Instantiate(obst, new Vector3(Random.Range(-width / 2, width / 2), transform.position.y, 0), Quaternion.identity);
            obs.GetComponent<Rigidbody2D>().AddForce(Vector2.down * GameManager.instance.speed);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        }
    }
}
