using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public float countdownDuration = 3f;
    public AudioSource music;
    private bool isPaused = false;

    private void Start()
    {
        ResumeGame(); // Ensure the game starts unpaused
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        pauseScreen.SetActive(true); // Show the pause screen
        music.Pause();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        pauseScreen.SetActive(false); // Hide the pause screen
        music.Play();
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game before loading main menu
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    public void ResumeWithCountdown()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        int count = Mathf.CeilToInt(countdownDuration);
        while (count > 0)
        {
           // Debug.Log("Resuming in " + count + "...");
            yield return new WaitForSeconds(1f);
            count--;
        }
       // Debug.Log("Resuming game...");
        ResumeGame();
    }
}
