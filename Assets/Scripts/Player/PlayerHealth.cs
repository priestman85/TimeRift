using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float healthRegenRate = 1f;
    public float healthRegenDelay = 5f;

    [Header("Survival")]
    public float maxHunger = 100f;
    public float currentHunger;
    public float hungerRate = 2f;
    public float maxThirst = 100f;
    public float currentThirst;
    public float thirstRate = 3f;

    [Header("UI")]
    public Slider healthSlider;
    public Slider hungerSlider;
    public Slider thirstSlider;

    float regenTimer;
    bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;
        UpdateUI();
    }

    void Update()
    {
        if (isDead) return;

        // Survival mechanics
        currentHunger -= hungerRate * Time.deltaTime;
        currentThirst -= thirstRate * Time.deltaTime;

        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);

        // Damage from starvation/dehydration
        if (currentHunger <= 0 || currentThirst <= 0)
        {
            TakeDamage(5f * Time.deltaTime);
        }

        // Health regeneration
        regenTimer += Time.deltaTime;
        if (regenTimer >= healthRegenDelay && currentHealth < maxHealth)
        {
            currentHealth += healthRegenRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }

        UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        regenTimer = 0f;
        UpdateUI();

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();
    }

    public void Eat(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        UpdateUI();
    }

    public void Drink(float amount)
    {
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
        UpdateUI();
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player died!");
        // TODO: Game Over screen
    }

    void UpdateUI()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth / maxHealth;
        if (hungerSlider != null)
            hungerSlider.value = currentHunger / maxHunger;
        if (thirstSlider != null)
            thirstSlider.value = currentThirst / maxThirst;
    }
}
