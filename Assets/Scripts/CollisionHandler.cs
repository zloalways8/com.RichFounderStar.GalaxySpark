using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public HealthManager healthManager;
    public GameManager gameController;

    public AudioClip collisionEnemy, collisionPoint, collisionHeart;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision detected with: {collision.name}, Tag: {collision.tag}");

        if (collision.CompareTag("Gem"))
        {
            AudioManager.Instance.PlaySFX(collisionPoint);
            Debug.Log("Gem collected! Adding score.");
            gameController.AddScore(10);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Comet"))
        {
            AudioManager.Instance.PlaySFX(collisionEnemy);
            Debug.Log("Comet hit! Reducing health.");
            healthManager.TakeDamage(20);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Heart"))
        {
            AudioManager.Instance.PlaySFX(collisionHeart);

            Debug.Log("Heart collected! Restoring health.");
            healthManager.Heal(10);
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.LogWarning($"Unhandled collision with: {collision.name}, Tag: {collision.tag}");
        }
    }
}