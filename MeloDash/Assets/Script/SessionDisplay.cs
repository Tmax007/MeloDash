using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SessionDisplay : MonoBehaviour
{
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void OnConnectionSuccess(int sessionNum)
    {
        text.text = "Session " + sessionNum;
    }

    public void OnConnectionFail(string error)
    {
        text.text = error;
    }
}
