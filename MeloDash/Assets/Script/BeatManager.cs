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
        //Debug.Log("BeatManager Update method called.");
        // Calculate the current song time
        float currentTime = Time.time - songStartTime;

        // Debug log for current and next beat timestamps
        //Debug.Log("Current time: " + currentTime + ", Next beat timestamp: " + beatDataList[currentBeatIndex].timestamp);

        //Debug.Log("Current beat index: " + currentBeatIndex + ", Beat data count: " + beatDataList.Count);

        // Debug log for spawning enemy
        //Debug.Log("Spawning enemy for beat index: " + currentBeatIndex);


        // Check if the current time matches the next beat timestamp
        if (currentBeatIndex < beatDataList.Count && currentTime >= beatDataList[currentBeatIndex].timestamp)
        {
            //Debug.Log("Current time: " + currentTime + ", Next beat timestamp: " + beatDataList[currentBeatIndex].timestamp);

            // Ensure that the current beat index is valid
            if (currentBeatIndex >= 0 && currentBeatIndex < beatDataList.Count)
            {
                //Debug.Log("Spawning enemy for beat index: " + currentBeatIndex);

                // Trigger an action at this beat (e.g., spawn an enemy)
                SpawnEnemy(currentBeatIndex);

                // Move to the next beat
                currentBeatIndex++;
            }
            else
            {
                //Debug.LogError("Invalid beat index: " + currentBeatIndex);
            }
        }
    }

    void SpawnEnemy(int beatIndex)
    {
        // Get the associated enemy spawner index for this beat
        spawnerIndex = beatDataList[currentBeatIndex].spawnerIndex;

        //Debug.Log("Spawning enemy at spawner index: " + spawnerIndex);

        // Ensure that the enemySpawners list is not null and contains the spawner index
        //if (spawnerIndex >= 0 && spawnerIndex < enemySpawners.Count && enemySpawners[spawnerIndex] != null)
        //{
            // Spawn an enemy using the corresponding enemy spawner
        enemySpawners[spawnerIndex].SpawnVerticalEnemy();
        //}
        //else
        //{
        //    Debug.LogError("Invalid enemy spawner reference for beat index: " + beatIndex);
        //}
    }

    void PlaySong()
    {
        // Add a debug log to indicate that the PlaySong method is being called
        //Debug.Log("PlaySong method called.");

        // Play the song (e.g., using AudioSource.Play)
        songStartTime = Time.time; // Record the start time of the song

        // Add a debug log to print the song start time
        //Debug.Log("Song start time: " + songStartTime);
    }
}
