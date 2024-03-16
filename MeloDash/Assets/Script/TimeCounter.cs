using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void logTime()
    {
        TelemetryLogger.Log(this, "Time spent on tutorial: " + ((int)Time.timeSinceLevelLoad + 1));
    }
}
