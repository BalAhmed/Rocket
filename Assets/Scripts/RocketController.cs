using System.Collections;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    public float upSpeed = 0.5f;
    public float moveSpeed = 1.0f;
    public float tiltAngle = 20f; // Eğim
    public float tiltSpeed = 5f;  // Dönüş Hızı
    public float maxFuel = 100f;
    public float currentFuel;
    public float fuelConsumpttionRate = 0.50f;
    public float score = 0;
    public Transform rocketTransform;
    public float baseHight;
    public TMP_Text fuelText;
    public TMP_Text scoreText;
    public Button playButton;
    public GameObject engine;
    public static bool canPlay = false;
    public int timeRemaining = 3;
    public TMP_Text playButtonText;

    private Rigidbody2D rb;
    private float targetRotationZ = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentFuel = maxFuel;
        baseHight = rocketTransform.position.y;
        playButtonText.text = ("Tap to Play");
    }

    void Update()
    {
        if (canPlay)
        {
            // Yukar� do�ru sabit h�zla hareket
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, upSpeed);


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (touch.position.x < Screen.width / 2)
                        {
                            rb.linearVelocity = new Vector2(-moveSpeed, upSpeed);
                            targetRotationZ = tiltAngle; // sola e�im
                        }

                        if (touch.position.x > Screen.width / 2)
                        {
                            rb.linearVelocity = new Vector2(moveSpeed, upSpeed);
                            targetRotationZ = -tiltAngle; // sa�a e�im
                        }
                        break;

                    case TouchPhase.Ended:
                        rb.linearVelocity = new Vector2(0f, upSpeed);
                        targetRotationZ = 0f; // d�z hale geri d�n
                        break;
                }


            }
            if (currentFuel > 0)
            {
                currentFuel -= fuelConsumpttionRate * Time.deltaTime;
                fuelText.text = ("Fuel:") + ((int)currentFuel).ToString();
                float height = rocketTransform.position.y - baseHight;
                score = Mathf.FloorToInt(height);
                scoreText.text = score.ToString();

                if (currentFuel <= 0)
                {
                    currentFuel = 0;
                    Debug.Log("No Fuel!");
                }
            }

            // D�n��� yava��a hedef a��ya do�ru yap
            float currentZ = transform.rotation.eulerAngles.z;
            if (currentZ > 180f) currentZ -= 360f; // A��y� -180 ile 180 aras� normalize et
            float newZ = Mathf.Lerp(currentZ, targetRotationZ, Time.deltaTime * tiltSpeed);
            transform.rotation = Quaternion.Euler(0f, 0f, newZ);
        }

    }

    public void StarGame()
    {
        playButton.gameObject.SetActive(false);
        playButtonText.gameObject.SetActive(true);
        StartCoroutine(Countdown());
        if (timeRemaining == 0)
        {
            engine.gameObject.SetActive(true);
            playButtonText.gameObject.SetActive(false);
            canPlay = true;
        }
    }
    IEnumerator Countdown()
    {
        while (timeRemaining > 0)
        {
            playButtonText.text = timeRemaining.ToString();
            yield return new WaitForSeconds(1f); // 1 saniye bekle
            timeRemaining--;
            if(timeRemaining == 0)
            {
                StarGame();
            }
        }

    }
}
