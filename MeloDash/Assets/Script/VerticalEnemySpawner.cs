using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemySpawner : MonoBehaviour
{
    public Transform snapPoint;
    public GameObject enemyPrefab;
    public float spawnInterval = 2.0f;
    public float descentSpeed = 2.0f;

    private float spawnTimer = 0;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    void SpawnEnemy()
    {
        // Instantiate a new enemy at the spawner's position
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

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
