using UnityEngine;

public class CameraControll : MonoBehaviour
{

    public GameObject rocket;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rocket = GameObject.Find("Rocket");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (transform.rotation.x ,rocket.transform.position.y + 4, rocket.transform.position.z -10);
    }
}
