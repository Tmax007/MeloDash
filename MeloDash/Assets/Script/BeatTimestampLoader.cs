using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class BeatTimestampLoader : MonoBehaviour
{
    // Define the structure for beat data
    [System.Serializable]
    public struct BeatData
    {
        public float timestamp;
        public int spawnerIndex;
    }

    // Public reference to the CSV file containing beat timestamps
    public TextAsset beatTimestampsCSV;

    // List to store beat data (timestamp and associated enemy spawner index)
    public List<BeatData> beatDataList = new List<BeatData>();

    // Reference to the enemy spawners
    public List<VerticalEnemySpawner> enemySpawners;

    void Start()
    {
        if (beatTimestampsCSV == null) { Debug.LogError("No CSV file assigned!"); return; }
        LoadBeatTimestamps();
    }

    void LoadBeatTimestamps()
    {
        //Debug.Log("Loading beat timestamps...");
        // Split the CSV text into lines
        string[] lines = beatTimestampsCSV.text.Split('\n');

        // Iterate over each line in the CSV
        foreach (string line in lines)
        {
            // Split the line into beat and timestamp
            string[] values = line.Trim().Split(',');

            if (values.Length >= 3)
            {
                // Parse the timestamp string to a float
                if (float.TryParse(values[1], out float timestamp) && int.TryParse(values[2], out int spawnerIndex))
                {
                    // Add the timestamp and spawner index to the list
                    BeatData beatData = new BeatData();
                    beatData.timestamp = timestamp;
                    beatData.spawnerIndex = spawnerIndex;
                    beatDataList.Add(beatData);

                    // Debug log the loaded beat data
                    //Debug.Log("Loaded beat data: Timestamp=" + timestamp + ", SpawnerIndex=" + spawnerIndex);
                }
                else
                {
                    //Debug.LogError("Invalid timestamp format: " + values[1]);
                }
            }
        }
        //Debug.Log("Beat timestamps loaded.");
    }
}
