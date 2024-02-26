using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private LayerMask enemyLayer;

    GameObject player;
    Shooting playershoot;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetEnemyLayer(LayerMask enemyLayer)
    {
        this.enemyLayer = enemyLayer;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        playershoot = player.GetComponent<Shooting>();
    }

    void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Check for collision with enemies
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.1f, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            // TO SHAUN: Just changed the layer for the destructible enemy so that only it will be destroyed

            // Destroy the enemy
            Destroy(hit.gameObject);

            playershoot.enemiesDestroyed++;

            //Destroy the projectile
            Destroy(gameObject);
        }
    }
}
