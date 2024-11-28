using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public LevelDataSO[] levels; // Массив данных уровней
    private const string ProgressKey = "LevelProgress";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Проверяем, есть ли сохраненные данные
            if (!PlayerPrefs.HasKey(ProgressKey))
            {
                InitializeFirstRun(); // Разблокируем уровень 0 при первом запуске
            }
            else
            {
                LoadLevelProgress(); // Загружаем сохраненные данные
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevelData(int levelIndex)
    {
        if (levelIndex < levels.Length)
        {
            GameData.CurrentLevel = levels[levelIndex].levelIndex;
            GameData.LevelScoreGoal = levels[levelIndex].scoreGoal;
            GameData.LevelTimeLimit = levels[levelIndex].timeLimit;
        }
        else
        {
            Debug.LogError($"Level {levelIndex} does not exist in LevelManager!");
        }
    }


    private void InitializeFirstRun()
    {
        // Открываем только уровень 0
        UnlockLevel(0);
        SaveLevelProgress();
    }

    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex < levels.Length)
        {
            levels[levelIndex].isUnlocked = true;
        }
    }

    public void CompleteLevel(int levelIndex)
    {
        if (levelIndex < levels.Length)
        {
            // Разблокируем следующий уровень
            int nextLevelIndex = levelIndex + 1;
            if (nextLevelIndex < levels.Length)
            {
                UnlockLevel(nextLevelIndex);
            }
            SaveLevelProgress();
        }
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex < levels.Length)
        {
            return levels[levelIndex].isUnlocked;
        }
        return false;
    }

    public void SaveLevelProgress()
    {
        string progress = "";
        foreach (var level in levels)
        {
            progress += (level.isUnlocked ? "1" : "0") + ";";
        }
        PlayerPrefs.SetString(ProgressKey, progress);
        PlayerPrefs.Save();
    }

    private void LoadLevelProgress()
    {
        string progress = PlayerPrefs.GetString(ProgressKey, "1;0;"); // Уровень 0 открыт по умолчанию
        string[] levelStates = progress.Split(';');

        for (int i = 0; i < levelStates.Length; i++)
        {
            if (i < levels.Length)
            {
                levels[i].isUnlocked = levelStates[i] == "1";
            }
        }

        LogLevelStatus();
    }

    public void LogLevelStatus()
    {
        Debug.Log("Level Status:");
        for (int i = 0; i < levels.Length; i++)
        {
            string status = levels[i].isUnlocked ? "Unlocked" : "Locked";
            Debug.Log($"Level {i}: {status}");
        }
    }
}
