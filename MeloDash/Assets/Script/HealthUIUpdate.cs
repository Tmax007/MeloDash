using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HealthUIUpdate : MonoBehaviour
{

    public Health playerHealth;

    TextMeshProUGUI text;

    SceneLoader loader;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        loader = GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Health: " + playerHealth.healthNum.ToString();

        if(playerHealth.healthNum <= 0)
        {
            loader.loadScene("GameOverScreen");
        }
    }

}
