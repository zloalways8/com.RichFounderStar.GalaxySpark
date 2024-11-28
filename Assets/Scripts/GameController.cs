using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int targetScore = 500; // Необходимые очки
    private int currentScore = 0;
    private bool levelCompleted = false;

    public UIManager uiManager;

    public void AddScore(int score)
    {
        if (levelCompleted) return;

        currentScore += score;
        uiManager.UpdateScore(currentScore, targetScore);

        if (currentScore >= targetScore)
        {
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        levelCompleted = true;
        Debug.Log("Level Completed!");

        // Завершаем текущий уровень через LevelManager
        LevelManager.Instance.CompleteLevel(GameData.CurrentLevel);

        // Переход на экран победы
        SceneManager.LoadScene("WinScene");
    }

    public void FailLevel()
    {
        levelCompleted = true;
        Debug.Log("Level Failed!");
        SceneManager.LoadScene("GameOverScene");
    }
}
