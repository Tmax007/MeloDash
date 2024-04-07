using UnityEngine;

public class VerticalEnemySpawner : MonoBehaviour
{
    public Transform snapPoint;
    public GameObject enemyPrefab;
    public Renderer spawnerRenderer; // Reference to the spawner's renderer component
    public Color defaultColor; // Default color of the spawner
    public Color highlightColor = Color.red; // Color to highlight the spawner when enemies are spawned
    public float descentSpeed = 2.0f;

    public float[] beatTimestamps; // Array to store beat timestamps
    public int currentBeatIndex = 0; // Index of the current beat

    // Index of the spawner
    public int spawnerIndex;

    public void SpawnVerticalEnemy()
    {
        // Instantiate a new regular enemy at the spawner's position
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

        // Highlight the spawner when an enemy is spawned
        HighlightSpawner();
    }

    void HighlightSpawner()
    {
        if (spawnerRenderer != null)
        {
            spawnerRenderer.material.color = highlightColor;
            Invoke("ResetSpawnerColor", 0.5f); // Reset the color after 0.5 second
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
