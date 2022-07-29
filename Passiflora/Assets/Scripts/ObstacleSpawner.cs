using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner instance;
    public GameObject first, second, third, fourth;
    public Dictionary<int, GameObject> obstPrefabs;

    public List<GameObject> obst;

    private List<GameObject> obsts;

    private System.Random rnd;
    float width;

    private void Awake()
    {

        obstPrefabs = new Dictionary<int, GameObject>();
        obst = new List<GameObject>();

        obstPrefabs.Add(0, first);
        obstPrefabs.Add(200, second);
        obstPrefabs.Add(300, third);
        obstPrefabs.Add(400, fourth);

    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        obsts = new List<GameObject>();
        //width = GetComponent<BoxCollider2D>().size.x;
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        //print(width + "/" + GetComponent<BoxCollider2D>().size.x);
        StartCoroutine(Spawn());

        rnd = new System.Random();
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            GameObject obs = Instantiate(obst[rnd.Next(obst.Count)], new Vector3(Random.Range(-width / 2, width / 2), transform.position.y, 0), Quaternion.identity);
            obs.GetComponent<Rigidbody2D>().AddForce(Vector2.down * GameManager.instance.speed);

            obsts.Add(obs);
        }
    }

    public void Clean()
    {
        foreach (var obs in obsts)
        {
            Destroy(obs);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        }
    }

    public void FillPool(int score)
    {
        if (obstPrefabs.ContainsKey(score))
        {
            obst.Add(obstPrefabs[score]);
        }
    }
}
