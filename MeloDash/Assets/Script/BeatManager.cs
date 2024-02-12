using UnityEngine;

public class BeatManager : MonoBehaviour
{
    // Beat and timestamp of the beat in seconds.millisecond format. Values are separated by a dot
    public string[] beatTimestamps;

    private float songStartTime;
    // Index of the current beat
    private int currentBeatIndex;

    // Reference to the enemy spawner
    public VerticalEnemySpawner[] enemySpawners;

    void Start()
    {
        // Start playing the song
        PlaySong();

        // Set the initial beat index
        currentBeatIndex = 0;
    }

    void Update()
    {
        // Calculate the current song time
        float currentTime = Time.time - songStartTime;

        // Check if the current time matches the next beat timestamp
        if (currentBeatIndex < beatTimestamps.Length && currentTime >= ParseTimeStamp(beatTimestamps[currentBeatIndex]))
        {
            // Trigger an action at this beat (e.g., spawn an enemy)
            SpawnEnemy();

            // Move to the next beat
            currentBeatIndex++;
        }
    }

    void PlaySong()
    {
        // Play the song (e.g., using AudioSource.Play)
        songStartTime = Time.time; // Record the start time of the song
    }

    void SpawnEnemy()
    {
        // Spawn an enemy using the enemy spawner
        if (enemySpawners != null)
        {
            foreach (VerticalEnemySpawner spawner in enemySpawners)
            {
                spawner.SpawnVerticalEnemy();
            }
        }
        else
        {
            Debug.LogError("EnemySpawner reference is missing!");
        }
    }

    float ParseTimeStamp(string timestamp)
    {
        string[] parts = timestamp.Split('.');
        if (parts.Length == 2 && float.TryParse(parts[0], out float seconds) && float.TryParse(parts[1], out float milliseconds))
        {
            return seconds + milliseconds / 1000f; // Convert milliseconds to seconds
        }
        else
        {
            Debug.LogError("Invalid timestamp format: " + timestamp);
            return 0f;
        }
    }
}
