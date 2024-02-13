using UnityEngine;

public class VerticalEnemySpawner : MonoBehaviour
{
    public Transform snapPoint;
    public GameObject enemyPrefab;
    public float descentSpeed = 2.0f;

    public float[] beatTimestamps; // Array to store beat timestamps
    public int currentBeatIndex = 0; // Index of the current beat

    // Index of the spawner
    public int spawnerIndex;

    void Start()
    {
        // I'll initialize it with an empty array
        
        //Debug.Log(currentBeatIndex);
        //Debug.Log("VerticalEnemySpawner Start method called.");
    }

    void Update()
    {
        //Debug.Log("VerticalEnemySpawner Update method called.");

        // Check if there are beat timestamps and if the current beat index is within bounds
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        // Check if it's time to spawn an enemy based on the current beat timestamp
        if (Time.time >= beatTimestamps[currentBeatIndex])
        {
            Debug.Log("SpawnVerticalEnemy called.");
            //SpawnVerticalEnemy();
           // currentBeatIndex++; // Move to the next beat
        }

    }

    public void SpawnVerticalEnemy()
    {
        Debug.Log("Spawning enemy at spawner index: " + spawnerIndex);

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
        else
        {
            Debug.LogError("EnemyMovement component not found on the spawned enemy.");
        }
    }
}
