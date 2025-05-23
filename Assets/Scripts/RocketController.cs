using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

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

    private Rigidbody2D rb;
    private float targetRotationZ = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentFuel = maxFuel;
        baseHight = rocketTransform.position.y;
    }

    void Update()
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
