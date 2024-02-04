using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private LayerMask enemyLayer;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetEnemyLayer(LayerMask enemyLayer)
    {
        this.enemyLayer = enemyLayer;
    }

    void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Check for collision with enemies
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.1f, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            // Destroy the enemy
            Destroy(hit.gameObject);

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
