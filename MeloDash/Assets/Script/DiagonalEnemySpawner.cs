using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalEnemySpawner : MonoBehaviour
{
    public Transform[] lanes;
    public GameObject enemyPrefab;
    public float[] laneSpawnRates;

    public float descentSpeed = 2.0f;

    private float[] laneTimers;

    void Start()
    {
        // Initialize the lane timers array with length of the lanes array
        laneTimers = new float[lanes.Length];
    }

    void Update()
    {
        // Iterate over each lane
        for (int i = 0; i < lanes.Length; i++)
        {
            // Increment timer 
            laneTimers[i] += Time.deltaTime;

            // Check if it's time to spawn an enemy 
            if (laneTimers[i] >= laneSpawnRates[i])
            {
                SpawnEnemy(i);

                // Reset timer
                laneTimers[i] = 0;
            }
        }
    }

    void SpawnEnemy(int laneIndex)
    {
        // Instantiate a new enemy
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Get Enemy component
        Enemy enemyMovement = newEnemy.GetComponent<Enemy>();

        if (enemyMovement != null)
        {
            // Set destination of enemy to the position of current lane
            enemyMovement.SetDestination(lanes[laneIndex].position);

            // Set descent speed of enemy
            enemyMovement.SetDescentSpeed(descentSpeed);
        }
    }
}
