using UnityEngine;
using System.Collections.Generic;

public class BeatManager : MonoBehaviour
{
    // List to store beat data (timestamp and associated enemy spawner index)
    public List<BeatTimestampLoader.BeatData> beatDataList;
    public BeatTimestampLoader loader;

    private float songStartTime;
    public int currentBeatIndex;
    public int spawnerIndex;
    // Reference to the enemy spawners
    public List<VerticalEnemySpawner> enemySpawners;

    void Start()
    {
        // Start playing the song
        PlaySong();

        // Set the initial beat index
        currentBeatIndex = 0;

        beatDataList = loader.beatDataList;
        int l = beatDataList.Count;
        foreach (var spawner in enemySpawners)
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
                SpawnEnemy(currentBeatIndex);

                // Move to the next beat
                currentBeatIndex++;
            }
        }
    }

    void SpawnEnemy(int beatIndex)
    {
        // Get the associated enemy spawner index for this beat
        spawnerIndex = beatDataList[currentBeatIndex].spawnerIndex;

        // Spawn an enemy using the corresponding enemy spawner
        enemySpawners[spawnerIndex].SpawnVerticalEnemy();
    }

    void PlaySong()
    {
        // Play the song (e.g., using AudioSource.Play)
        songStartTime = Time.time; // Record the start time of the song
    }
}
