using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения объекта

    void Update()
    {
        // Движение влево
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // Удаляем объект, если он выходит за пределы экрана
        if (transform.position.x < -15f) // Допустим, предел экрана — -15
        {
            Destroy(gameObject);
        }
    }
}