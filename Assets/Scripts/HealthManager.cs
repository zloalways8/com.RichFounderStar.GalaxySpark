using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное здоровье
    private int currentHealth;

    public GameManager gameController; // Для вызова поражения
    public UIManager uiManager;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        uiManager.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            gameController.GameOver(); // Вызываем поражение
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        uiManager.UpdateHealth(currentHealth, maxHealth);
    }
}