using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompileScores : MonoBehaviour
{
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI highScore;

    // Start is called before the first frame update
    void Start()
    {
        if(Init.score > Init.highScore)
        {
            Init.highScore = Init.score;
        }

        finalScore.text += Init.score;
        highScore.text += Init.highScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
