using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI display;

    public void onConnectionSuccess(int sessionNum)
    {
        display.text = $"Session: {sessionNum}";
    }

    public void onConnectionFail(string errorMes)
    {
        display.text = errorMes ;
    }
}
