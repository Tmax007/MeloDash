using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class BeatTimestampLoader : MonoBehaviour
{
    // Public reference to the CSV file containing beat timestamps
    public TextAsset beatTimestampsCSV;

    // Beat and timestamp of the beat in seconds.millisecond format. Values are separated by a comma
    public List<float> beatTimestamps = new List<float>();

    void Start()
    {
        if (beatTimestampsCSV != null)
        {
            LoadBeatTimestamps();
        }
        else
        {
            Debug.LogError("No CSV file assigned!");
        }
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

            if (values.Length >= 2)
            {
                // Parse the timestamp string to a float
                if (float.TryParse(values[1], out float timestamp))
                {
                    // Add the timestamp to the list
                    beatTimestamps.Add(timestamp);
                }
                else
                {
                    Debug.LogError("Failed to parse timestamp: " + values[1]);
                }
            }
        }
    }
}
