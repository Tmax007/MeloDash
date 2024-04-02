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

        Invoke("endGame", 125);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.healthNum <= 0 && loader != null)
        {
            loader.loadScene("GameOverScreen");
        }

        if(regenBar == null)
        {
            regenBar.fillAmount = playerHealth.currentTime / playerHealth.regenTime;
        }
    }

    void UpdateUINumber()
    {
        text.text = startText + playerHealth.healthNum.ToString();
        Debug.Log(playerHealth.healthNum);
    }

    void endGame()
    {
        loader.loadScene("EndScreen");
    }

}
