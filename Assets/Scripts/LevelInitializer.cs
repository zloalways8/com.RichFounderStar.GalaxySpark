// LevelInitializer.cs
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    public GameObject player; // Игрок
    public Transform playerStartPosition; // Начальная позиция игрока
    public ObjectSpawner objectSpawner; // Спавнер объектов

    void Start()
    {
        InitializeLevel();
    }

    void InitializeLevel()
    {
        // Перемещаем игрока на стартовую позицию
        if (player != null && playerStartPosition != null)
        {
            player.transform.position = playerStartPosition.position;
        }

        // Настраиваем спавнер объектов
        if (objectSpawner != null)
        {
            objectSpawner.SetupSpawner();
        }

        Debug.Log("Level initialized!");
    }
}