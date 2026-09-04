using UnityEngine;

public class TimeAnomaly : MonoBehaviour
{
    [Header("Settings")]
    public float damagePerSecond = 20f;
    public float pulseSpeed = 2f;
    public float pulseIntensity = 0.1f;
    public float rotationSpeed = 30f;

    [Header("Visual")]
    public ParticleSystem anomalyParticles;
    public Light anomalyLight;
    public Color anomalyColor = new Color(0.5f, 0f, 0.8f);

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip anomalyHum;

    float timeElapsed;
    bool isActive = true;

    void Start()
    {
        if (anomalyLight != null)
        {
            anomalyLight.color = anomalyColor;
        }

        if (anomalyParticles != null)
        {
            anomalyParticles.Play();
        }
    }

    void Update()
    {
        if (!isActive) return;

        timeElapsed += Time.deltaTime;

        // Pulsate light
        if (anomalyLight != null)
        {
            float intensity = 1f + Mathf.Sin(timeElapsed * pulseSpeed) * pulseIntensity;
            anomalyLight.intensity = intensity;
        }

        // Rotate
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Scale pulsate
        float scale = 1f + Mathf.Sin(timeElapsed * pulseSpeed * 0.5f) * 0.05f;
        transform.localScale = Vector3.one * scale;
    }

    void OnTriggerStay(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered time anomaly zone!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exited time anomaly zone!");
        }
    }

    public void Activate()
    {
        isActive = true;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
}
