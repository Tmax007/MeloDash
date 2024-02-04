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

    // Update the score when an enemy reaches the snap point without colliding with the player
    public void UpdateScore()
    {
        // Increment the score
        score++;

        // Update the UI to display the new score
        scoreText.text = "Score: " + score.ToString();
    }
}