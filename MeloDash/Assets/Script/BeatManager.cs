using UnityEngine;
using System.Collections.Generic;

public class BeatManager : MonoBehaviour
{
    // List to store beat data (timestamp and associated enemy spawner indices)
    public List<BeatTimestampLoader.BeatData> beatDataList;
    public BeatTimestampLoader loader;

    private float songStartTime;
    public int currentBeatIndex;
    public int[] regularSpawnerIndices;
    public int[] destructibleSpawnerIndices;
    // Reference to the enemy spawners
    public List<VerticalEnemySpawner> enemySpawners;
    public List<DiagonalEnemySpawner> destructibleEnemySpawners;

    void Start()
    {
        // Start playing the song
        PlaySong();

        // Set the initial beat index
        currentBeatIndex = 0;

        beatDataList = loader.beatDataList;
        int l = beatDataList.Count;

        // Initialize beat timestamps for vertical enemy spawners
        foreach (var spawner in enemySpawners)
        {
            spawner.beatTimestamps = new float[l];
            for (int i = 0; i < l; i++)
            {
                spawner.beatTimestamps[i] = beatDataList[i].timestamp;
            }
        }

        // Initialize beat timestamps for diagonal enemy spawners
        foreach (var spawner in destructibleEnemySpawners)
        {
            spawner.beatTimestamps = new float[l];
            for (int i = 0; i < l; i++)
            {
                spawner.beatTimestamps[i] = beatDataList[i].timestamp;
            }
        }
    }

    void Update()
    {
        // Calculate the current song time
        float currentTime = Time.time - songStartTime;

        // Check if the current time matches the next beat timestamp
        if (currentBeatIndex < beatDataList.Count && currentTime >= beatDataList[currentBeatIndex].timestamp)
        {
            // Ensure that the current beat index is valid
            if (currentBeatIndex >= 0 && currentBeatIndex < beatDataList.Count)
            {
                // Trigger an action at this beat (e.g., spawn an enemy)
                SpawnEnemies(currentBeatIndex);

                // Move to the next beat
                currentBeatIndex++;
            }
        }
    }

    void SpawnEnemies(int beatIndex)
    {
        // Get the associated enemy spawner indices for this beat
        regularSpawnerIndices = beatDataList[currentBeatIndex].regularEnemySpawnerIndices;
        destructibleSpawnerIndices = beatDataList[currentBeatIndex].destructibleEnemySpawnerIndices;

        // Spawn regular enemies using the corresponding enemy spawners
        foreach (int index in regularSpawnerIndices)
        {
            // Check if the index is valid for vertical enemy spawners
            if (index >= 0 && index < enemySpawners.Count)
            {
                enemySpawners[index].SpawnVerticalEnemy();
            }
            else
            {
                Debug.LogError("Invalid regular enemy spawner index: " + index);
            }
        }

        // Spawn destructible enemies using the corresponding enemy spawners
        foreach (int index in destructibleSpawnerIndices)
        {
            // Check if the index is valid for destructible enemy spawners
            if (index >= 0 && index < destructibleEnemySpawners.Count)
            {
                destructibleEnemySpawners[index].SpawnDestructibleEnemy();
            }
            else
            {
                Debug.LogError("Invalid destructible enemy spawner index: " + index);
            }
        }
    }

    void PlaySong()
    {
        // Play the song
        songStartTime = Time.time; // Record the start time of the song
    }
}
