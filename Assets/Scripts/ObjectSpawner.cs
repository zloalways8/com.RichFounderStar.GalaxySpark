// ObjectSpawner.cs
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objects; // Камни, кометы, сердечки
    public float spawnRate = 1f; // Частота спавна
    public float spawnAreaHeight = 5f; // Высота области спавна
    public float spawnPositionX = 10f; // Позиция по X

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    public void SetupSpawner()
    {
        // Можно добавить конфигурацию из настроек уровня
        Debug.Log("Spawner configured!");
    }

    void SpawnObject()
    {
        int index = Random.Range(0, objects.Length);
        float spawnY = Random.Range(-spawnAreaHeight, spawnAreaHeight);
        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnY, 0);

        if (objects[index] != null)
        {
            GameObject spawnedObject = Instantiate(objects[index], spawnPosition, Quaternion.identity);

            // Добавляем движение объекту
            ObjectMover mover = spawnedObject.AddComponent<ObjectMover>();
            mover.moveSpeed = Random.Range(3f, 6f); // Случайная скорость для разнообразия
        }
    }
}