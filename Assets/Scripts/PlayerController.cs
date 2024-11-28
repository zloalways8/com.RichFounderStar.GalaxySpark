using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 offset;

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        newPosition.z = 0; // Ограничение движения по оси Z
        transform.position = newPosition;
    }
}