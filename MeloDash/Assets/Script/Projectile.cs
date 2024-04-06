using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    public LayerMask enemyLayer;

    GameObject player;
    Shooting playershoot;

    Health targetHealth;

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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            // TO SHAUN: Just changed the layer for the destructible enemy so that only it will be destroyed

            targetHealth = hit.GetComponent<Health>();

            if(targetHealth.healthNum == 0)
            {
                playershoot.enemiesDestroyed++;
            }

            //Destroy the projectile
        }
    }
}
