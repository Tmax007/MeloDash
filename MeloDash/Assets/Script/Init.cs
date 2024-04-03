using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{

    public static bool beenToTutorial;

    // Start is called before the first frame update
    void Start()
    {
        beenToTutorial = false;

        SceneLoader loader = gameObject.GetComponent<SceneLoader>();

        loader.loadScene("MainMenu");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
