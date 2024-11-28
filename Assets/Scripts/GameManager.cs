using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Win Window UI")]
    public GameObject winWindow; // Ссылка на окно победы
    public TMP_Text winScoreText; // TMP Text для отображения очков в окне победы
    public Slider winHealthSlider; // Слайдер здоровья в окне победы
    public TMP_Text winHealthText; // TMP Text для отображения здоровья в окне победы
    public Slider winTimeSlider; // Слайдер времени в окне победы
    public TMP_Text winTimeText; // TMP Text для отображения времени в окне победы

    [Header("Defeat Window UI")]
    public GameObject defeatWindow; // Ссылка на окно поражения
    public TMP_Text defeatScoreText; // TMP Text для отображения очков в окне поражения
    public Slider defeatHealthSlider; // Слайдер здоровья в окне поражения
    public TMP_Text defeatHealthText; // TMP Text для отображения здоровья в окне поражения
    public Slider defeatTimeSlider; // Слайдер времени в окне поражения
    public TMP_Text defeatTimeText; // TMP Text для отображения времени в окне поражения

    [Header("Game Variables")]
    public int currentScore = 0;
    public float timeRemaining;
    private bool levelCompleted = false;

    public int currentHealth = 100; // Текущее здоровье игрока
    public int maxHealth = 100; // Максимальное здоровье игрока

    public UIManager manager;

    public AudioClip winSound, loseSound;

    public GameObject Settings;
    void Awake()
    {
        // Находим UIManager
        manager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Start()
    {
        InitializeLevel();
    }

    void Update()
    {
        if (!levelCompleted)
        {
            // Уменьшаем оставшееся время
            timeRemaining -= Time.deltaTime;

            // Обновляем слайдер времени
            manager.UpdateTime(timeRemaining, GameData.LevelTimeLimit);

            // Проверяем, если время истекло
            if (timeRemaining <= 0)
            {
                GameOver();
            }
        }
    }

    public void OpenSettings()
    {
        Settings.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseSettings()
    {
        Settings.SetActive(false);
        Time.timeScale = 1;
    }
    public void InitializeLevel()
    {
        // Загружаем данные уровня
        LevelManager.Instance.LoadLevelData(GameData.CurrentLevel);

        // Загружаем итоговое HP
        maxHealth = PlayerPrefs.GetInt("TotalHP", 100); // Стартовое HP по умолчанию — 100
        currentHealth = maxHealth;

        // Инициализация переменных
        currentScore = 0;
        timeRemaining = GameData.LevelTimeLimit;

        // Обновляем UI
        manager.UpdateHealth(currentHealth, maxHealth);
        manager.UpdateTime(timeRemaining, GameData.LevelTimeLimit);
        manager.UpdateScore(currentScore, GameData.LevelScoreGoal);

        Debug.Log($"Level {GameData.CurrentLevel} initialized with Max HP: {maxHealth}");
    }

    public void AddScore(int score)
    {
        if (levelCompleted) return;

        currentScore += score;
        manager.UpdateScore(currentScore, GameData.LevelScoreGoal);

        if (currentScore >= GameData.LevelScoreGoal)
        {
            CompleteLevel();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        manager.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        manager.UpdateHealth(currentHealth, maxHealth);
    }

    public void CompleteLevel()
    {
        AudioManager.Instance.PlaySFX(winSound);
        LevelManager.Instance.CompleteLevel(GameData.CurrentLevel);

        winWindow.SetActive(true);

        levelCompleted = true;
        Debug.Log("Level Completed!");

        // Добавляем к балансу
        int balance = PlayerPrefs.GetInt("Balance", 0);
        balance += 150;
        PlayerPrefs.SetInt("Balance", balance);
        PlayerPrefs.Save();

        // Обновляем окно победы
        UpdateWindow(winScoreText, winHealthSlider, winHealthText, winTimeSlider, winTimeText);

        // Показываем окно победы
    }


    public void GameOver()
    {
        AudioManager.Instance.PlaySFX(winSound);

        defeatWindow.SetActive(true);

        // Обновляем окно поражения
        UpdateWindow(defeatScoreText, defeatHealthSlider, defeatHealthText, defeatTimeSlider, defeatTimeText);

        // Показываем окно поражения
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        if (LevelManager.Instance != null)
        {
            int lastUnlockedLevel = -1;

            // Находим последний разблокированный уровень
            for (int i = 0; i < LevelManager.Instance.levels.Length; i++)
            {
                if (LevelManager.Instance.IsLevelUnlocked(i))
                {
                    lastUnlockedLevel = i;
                }
            }

            if (lastUnlockedLevel != -1)
            {
                // Проверяем, что уровень разблокирован
                if (LevelManager.Instance.IsLevelUnlocked(lastUnlockedLevel))
                {
                    GameData.CurrentLevel = lastUnlockedLevel; // Устанавливаем текущий уровень
                    LevelManager.Instance.LoadLevelData(lastUnlockedLevel); // Загружаем данные уровня
                    SceneManager.LoadScene("GameScene"); // Загружаем сцену
                    Debug.Log($"Loading last unlocked level: {lastUnlockedLevel}");
                }
                else
                {
                    Debug.Log($"Level {lastUnlockedLevel} is locked!");
                }
            }
            else
            {
                Debug.LogError("No unlocked levels found!");
            }
        }
        else
        {
            Debug.LogError("LevelManager instance is missing!");
        }
    }

    public void GoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
    private void UpdateWindow(TMP_Text scoreText, Slider healthSlider, TMP_Text healthText, Slider timeSlider, TMP_Text timeText)
    {
        // Обновляем очки
        scoreText.text = $"{currentScore}/{GameData.LevelScoreGoal}";

        // Обновляем здоровье
        healthSlider.maxValue = maxHealth;
        healthSlider.value = manager.healthSlider.value;
        healthText.text = $"{manager.healthText.text}/{maxHealth}HP";

        // Обновляем время
        timeSlider.maxValue = GameData.LevelTimeLimit;
        timeSlider.value = manager.timeSlider.value;
        timeText.text = $"{Mathf.CeilToInt(timeRemaining)}sec";

        Time.timeScale = 0;
    }
}
