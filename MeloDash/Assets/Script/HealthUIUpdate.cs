using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIUpdate : MonoBehaviour
{

    public Health playerHealth;
    public Image regenBar;

    TextMeshProUGUI text;

    string startText;

    SceneLoader loader;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        loader = GetComponent<SceneLoader>();

        startText = text.text;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.healthNum <= 0)
        {
            loader.loadScene("GameOverScreen");
        }

        regenBar.fillAmount = playerHealth.currentTime / playerHealth.regenTime;

    }

    void UpdateUINumber()
    {
        text.text = startText + playerHealth.healthNum.ToString();
        Debug.Log(playerHealth.healthNum);
    }

}
