using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    // Singleton instance of ScoreManager
    public static ScoreManager Instance { get; private set; }

    /*void Awake()
    {
        // Singleton pattern to ensure only one instance of ScoreManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    //Update the score by increasing it by the amount specified in the scoreAdd integer, before applying it to the UI.
    public void UpdateScore(int scoreAdd)
    {
        // Increment the score
        score += scoreAdd;

        // Update the UI to display the new score
        scoreText.text = "Score: " + score.ToString();
    }
}