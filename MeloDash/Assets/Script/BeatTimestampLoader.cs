using UnityEngine;
using System.Collections.Generic;

public class BeatTimestampLoader : MonoBehaviour
{
    // Define the structure for beat data
    [System.Serializable]
    public struct BeatData
    {
        public float timestamp;
        public int[] spawnerIndices; // Change to int[] to store multiple spawn index values
    }

    // Public reference to the CSV file containing beat timestamps
    public TextAsset beatTimestampsCSV;

    // List to store beat data (timestamp and associated enemy spawner index)
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

            if (values.Length >= 3)
            {
                // Parse the timestamp string to a float
                if (float.TryParse(values[1], out float timestamp))
                {
                    // Parse the spawner index values to an array of ints
                    string[] indexValues = values[2].Trim().Split(';');
                    List<int> indices = new List<int>();
                    foreach (string indexStr in indexValues)
                    {
                        if (int.TryParse(indexStr, out int index))
                        {
                            indices.Add(index);
                        }
                    }

                    // Add the timestamp and spawner indices to the list
                    BeatData beatData = new BeatData();
                    beatData.timestamp = timestamp;
                    beatData.spawnerIndices = indices.ToArray();
                    beatDataList.Add(beatData);
                }
            }
        }
    }
}
