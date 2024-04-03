using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadScene(string sceneName)
    {
        if(sceneName == "MainGame" && Init.beenToTutorial == false)
        {
            SceneManager.LoadScene("Tutorial");
        }

        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
