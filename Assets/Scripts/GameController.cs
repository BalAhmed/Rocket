using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public GameObject rocket;
    public GameObject[] spawner;
    public GameObject[] obstacle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        int randomObstacleIndex = Random.Range(0, obstacle.Length);
        GameObject selectedObstacle = obstacle[randomObstacleIndex];

        Instantiate(selectedObstacle, selectedSpawnerPos, Quaternion.identity);
    }
}
