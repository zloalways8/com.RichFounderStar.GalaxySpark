using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject levelPanel; 
    public GameObject mainMenuPanel; 

    public AudioClip btnClick;
    void Start()
    {
        // Проверяем сохранённые данные
        if (LevelManager.Instance != null)
        {
            // Убедимся, что прогресс загружен
            LevelManager.Instance.LogLevelStatus();
        }
        else
        {
            Debug.LogError("LevelManager instance is missing!");
        }

        // Показываем главное меню по умолчанию
        ShowMainMenu();
    }

    public void StartGame()
    {
        // Загружаем последний разблокированный уровень
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
                LevelManager.Instance.LoadLevelData(lastUnlockedLevel);
                GameData.CurrentLevel = lastUnlockedLevel;

                LoadLevel(lastUnlockedLevel);
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


    public void OpenLevelsPanel()
    {
        AudioManager.Instance.PlaySFX(btnClick);
        // Переключение из главного меню в меню выбора уровней
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (levelPanel != null) levelPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.PlaySFX(btnClick);
        // Переключение из меню выбора уровней в главное меню
        if (levelPanel != null) levelPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    public void LoadLevel(int levelIndex)
    {

        // Проверяем, разблокирован ли уровень
        if (LevelManager.Instance != null && LevelManager.Instance.IsLevelUnlocked(levelIndex))
        {
            GameData.CurrentLevel = levelIndex; // Установить текущий уровень
            LevelManager.Instance.LoadLevelData(levelIndex);
            SceneManager.LoadScene("GameScene"); // Сцена с игрой
        }
        else
        {
            Debug.Log($"Level {levelIndex} is locked!");
        }
    }

    private void ShowMainMenu()
    {
        //AudioManager.Instance.PlaySFX(btnClick);

        // Убедитесь, что только главное меню активно
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (levelPanel != null) levelPanel.SetActive(false);
    }
}
