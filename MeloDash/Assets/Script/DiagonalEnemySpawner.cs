using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalEnemySpawner : MonoBehaviour
{
    public Transform snapPoint;
    public GameObject destructibleEnemyPrefab;
    public float descentSpeed = 2.0f;

    public Renderer spawnerRenderer;
    public Color defaultColor=Color.white;
    public Color highlightColor = Color.red;

    public float[] beatTimestamps; // Array to store beat timestamps
    public int currentBeatIndex = 0; // Index of the current beat

    // Index of the spawner
    public int spawnerIndex;

    public void SpawnDestructibleEnemy()
    {
        // Instantiate a new destructible enemy at the spawner's position
        GameObject newEnemy = Instantiate(destructibleEnemyPrefab, transform.position, Quaternion.identity);

        // Get the EnemyMovement component of the spawned enemy
        Enemy enemyMovement = newEnemy.GetComponent<Enemy>();

        // If EnemyMovement component exists
        if (enemyMovement != null)
        {
            // Set destination of the enemy to snap point's position
            enemyMovement.SetDestination(snapPoint.position);

            // Set descent speed of enemy
            enemyMovement.SetDescentSpeed(descentSpeed);

            // Highlight the spawner when an enemy is spawned
            HighlightSpawner();
        }
    }

    void HighlightSpawner()
    {
        if (spawnerRenderer != null)
        {
            spawnerRenderer.material.color = highlightColor;
            Invoke("ResetSpawnerColor", 0.5f); // Reset the color after 1 second
        }
    }

    void ResetSpawnerColor()
    {
        if (spawnerRenderer != null)
        {
            spawnerRenderer.material.color = defaultColor;
        }
    }
}

