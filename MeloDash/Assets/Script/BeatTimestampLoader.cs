using UnityEngine;
using System.Collections.Generic;

public class BeatTimestampLoader : MonoBehaviour
{
    // Define the structure for beat data
    [System.Serializable]
    public struct BeatData
    {
        public float timestamp;
        public int[] regularEnemySpawnerIndices; // Indices for regular enemy spawners
        public int[] destructibleEnemySpawnerIndices; // Indices for destructible enemy spawners
    }

    // Public reference to the CSV file containing beat timestamps
    public TextAsset beatTimestampsCSV;

    // List to store beat data (timestamp and associated enemy spawner indices)
    public List<BeatData> beatDataList = new List<BeatData>();

    void Start()
    {
        if (beatTimestampsCSV == null) { Debug.LogError("No CSV file assigned!"); return; }
        LoadBeatTimestamps();
    }

    void LoadBeatTimestamps()
    {
        // Split the CSV text into lines
        string[] lines = beatTimestampsCSV.text.Split('\n');

        // Iterate over each line in the CSV
        foreach (string line in lines)
        {
            // Split the line into beat and timestamp
            string[] values = line.Trim().Split(',');

            if (values.Length >= 4)
            {
                // Parse the timestamp string to a float
                if (float.TryParse(values[1], out float timestamp))
                {
                    // Parse the indices for regular enemies
                    string[] regularIndices = values[2].Trim().Split(';');
                    List<int> regularEnemyIndices = new List<int>();
                    foreach (string indexStr in regularIndices)
                    {
                        if (int.TryParse(indexStr, out int index))
                        {
                            regularEnemyIndices.Add(index);
                        }
                    }

                    // Parse the indices for destructible enemies
                    string[] destructibleIndices = values[3].Trim().Split(';');
                    List<int> destructibleEnemyIndices = new List<int>();
                    foreach (string indexStr in destructibleIndices)
                    {
                        if (int.TryParse(indexStr, out int index))
                        {
                            destructibleEnemyIndices.Add(index);
                        }
                    }

                    // Add the timestamp and spawner indices to the list
                    BeatData beatData = new BeatData();
                    beatData.timestamp = timestamp;
                    beatData.regularEnemySpawnerIndices = regularEnemyIndices.ToArray();
                    beatData.destructibleEnemySpawnerIndices = destructibleEnemyIndices.ToArray();
                    beatDataList.Add(beatData);
                }
            }
        }
    }
}

