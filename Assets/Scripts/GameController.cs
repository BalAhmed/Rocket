using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    
    public GameObject rocket;
    public GameObject[] spawner;
    public GameObject[] obstacle;
    public int poolSizePerType = 10;
    private List<GameObject> pool = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(GameObject prefab in obstacle)
        {
            for ( int i = 0; i < poolSizePerType; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
        rocket = GameObject.Find("Rocket");
        InvokeRepeating("SpwanerObstacle", 2,2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.rotation.x, rocket.transform.position.y + 4);
    }

    public void SpwanerObstacle()
    {
        int randomSpawnerIndex = Random.Range(0, spawner.Length);
        Vector3 selectedSpawnerPos = spawner[randomSpawnerIndex].transform.position;

        List<GameObject> inactiveObstacles = pool.FindAll(o => !o.activeInHierarchy);

        if (inactiveObstacles.Count == 0)
            return; // Hepsi aktifse hiçbir þey yapma

        // 3. Rastgele bir tanesini seç
        GameObject pooledObstacle = inactiveObstacles[Random.Range(0, inactiveObstacles.Count)];

        // 4. Pozisyonunu ayarla ve aktif et
        pooledObstacle.transform.position = selectedSpawnerPos;
        pooledObstacle.transform.rotation = Quaternion.identity;
        pooledObstacle.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.SetActive(false);
        }

    }
}
