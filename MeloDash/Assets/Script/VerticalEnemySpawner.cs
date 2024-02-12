using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemySpawner : MonoBehaviour
{
    public Transform[] lanes;
    public GameObject[] enemyPrefabs;
    public float[] laneSpawnRates; // Adjust spawn rates per lane\po

    public Transform snapPoint; // Snap point for vertical enemy spawner
    public GameObject enemyPrefab; // Prefab for vertical enemy spawner
    public float spawnInterval = 2.0f; // Spawn interval for vertical enemy spawner
    public float descentSpeed = 2.0f; // Descent speed for vertical enemy spawner

    private float[] laneTimers;
    private float spawnTimer = 0;

    void Start()
    {
        laneTimers = new float[lanes.Length];
    }

    void Update()
    {
       // UpdateVerticalEnemySpawner();
    }

    void UpdateVerticalEnemySpawner()
    {
        // Increment spawn timer for vertical enemy spawner
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn an enemy vertically
        if (spawnTimer >= spawnInterval)
        {
            SpawnVerticalEnemy();
            spawnTimer = 0;
        }
    }

    public void SpawnVerticalEnemy()
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