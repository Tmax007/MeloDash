using UnityEngine;

public class VerticalEnemySpawner : MonoBehaviour
{
    public Transform snapPoint;
    public GameObject enemyPrefab;
    public GameObject destructibleEnemyPrefab;
    public float descentSpeed = 2.0f;

    public float[] beatTimestamps; // Array to store beat timestamps
    public int currentBeatIndex = 0; // Index of the current beat

    //Bool determines whether or spawner instantiates destructible or regular enemies.
    public bool spawnDestructible;

    // Index of the spawner
    public int spawnerIndex;

    public void SpawnVerticalEnemy()
    {
        GameObject newEnemy;

        if (spawnDestructible)
        {
            // Instantiate a new destructible enemy at the spawner's position
            newEnemy = Instantiate(destructibleEnemyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            // Instantiate a new enemy at the spawner's position
            newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
        
        // Get the EnemyMovement component of the spawned enemy
        Enemy enemyMovement = newEnemy.GetComponent<Enemy>();

        // If EnemyMovement component exists
        if (enemyMovement != null)
        {
            // Set destination of the enemy to snap point's position
            enemyMovement.SetDestination(snapPoint.position);

            // Set descent speed of enemy
            enemyMovement.SetDescentSpeed(descentSpeed);
        }
    }
}
