using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float fireRate = 0.5f;
    public float projectileSpeed = 10f;
    public LayerMask enemyLayer;

    private float fireTimer = 0f;

    void Update()
    {
        // Increment fire timer
        fireTimer += Time.deltaTime;

        // Check if player presses the left mouse button
        if (Input.GetButtonDown("Fire1") && fireTimer >= fireRate)
        {
            // Fire projectile
            FireProjectile();

            // Reset fire timer
            fireTimer = 0f;
        }
    }

    void FireProjectile()
    {
        // Instantiate a new projectile at the fire point's position and rotation
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Projectile component of the projectile
        Projectile projectile = newProjectile.GetComponent<Projectile>();

        // Check if the Projectile component exists
        if (projectile != null)
        {
            // Set the enemy layer mask for the projectile
            projectile.SetEnemyLayer(enemyLayer);

            // Set the speed of the projectile
            projectile.SetSpeed(projectileSpeed);
        }
        else
        {
            Debug.LogWarning("Projectile component not found in projectile prefab.");
        }
    }
}
