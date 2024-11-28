using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Health UI")]
    public Slider healthSlider; // Слайдер для здоровья
    public TMP_Text healthText; // Текст для здоровья

    [Header("Time UI")]
    public Slider timeSlider; // Слайдер для времени
    public TMP_Text timeText; // Текст для времени

    [Header("Score UI")]
    public TMP_Text scoreText; // Текст для очков

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthSlider != null)
        {
            // Устанавливаем текущее значение слайдера
            healthSlider.value = currentHealth;

            // Обновляем текст здоровья
            healthText.text = $"{currentHealth}HP";
        }
        else
        {
            Debug.LogError("Health Slider is not assigned in UIManager!");
        }
    }

    public void UpdateTime(float timeRemaining, float maxTime)
    {
        if (timeSlider != null)
        {
            // Устанавливаем максимальное значение слайдера времени
            timeSlider.maxValue = maxTime;

            // Устанавливаем текущее значение слайдера
            timeSlider.value = timeRemaining;

            // Обновляем текст времени, отображая оставшееся время как целое число
            timeText.text = $"{Mathf.CeilToInt(timeRemaining)}sec";
        }
        else
        {
            Debug.LogError("Time Slider is not assigned in UIManager!");
        }
    }


    public void UpdateScore(int currentScore, int targetScore)
    {
        if (scoreText != null)
        {
            // Обновляем текст очков
            scoreText.text = $"{currentScore:000}/{targetScore}";
        }
        else
        {
            Debug.LogError("Score Text is not assigned in UIManager!");
        }
    }
}
