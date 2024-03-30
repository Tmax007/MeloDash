using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    //Integer represents the current state of the tutorial (switched to int from enum because it's easier to transition between them with the buttons).
    //0 = dodgeTest, 1 = shootingTest, 2 = LSDTest.
    public int state;

    //Reference for the scripts used by tutorial's enemy spawner.
    public VerticalEnemySpawner verticalEnemySpawner;
    public DiagonalEnemySpawner diagonalEnemySpawner;

    //Reference for the player.
    public GameObject player;

    //Reference to multiple player functions that'll be toggled on & off throughout tutorial.
    LastSecondZone LSD;
    Shooting shoot;

    //Reference to rectangle child object that's used to visualize the size of the last-second zone.
    GameObject LSDVisualizer;

    public TextMeshProUGUI tutorialText;

    public Button nextButton;
    public Button prevButton;
    public Button exitButton;

    public float timeTillSpawn;
    float originalTime;

    bool spawnDestructibleEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        LSD = player.GetComponent<LastSecondZone>();
        shoot = player.GetComponent<Shooting>();

        LSDVisualizer = player.transform.GetChild(1).gameObject;

        originalTime = timeTillSpawn;
    }

    // Update is called once per frame
    void Update()
    {

        timeTillSpawn -= Time.deltaTime;

        if(timeTillSpawn <= 0 ) 
        { 
            if(spawnDestructibleEnemy )
            {
                diagonalEnemySpawner.SpawnDestructibleEnemy();
            }
            else
            {
                verticalEnemySpawner.SpawnVerticalEnemy();
            }

            timeTillSpawn = originalTime;
        
        }

        //Change tutorial text if in the dodgeTest state.
        if(state == 0)
        {
            tutorialText.text = "A/left arrow key and B/right arrow key to move left and right respectively. Dodge the orbs coming from the top of the screen.";
            prevButton.interactable = false;
        }
        else
        {
            prevButton.interactable = true;
        }

        //Set the spawner to spawn destructible enemies, change tutorial text, & enable player shooting if in the shootingTest state.
        if(state == 1)
        {
            //verticalEnemySpawner.spawnDestructible = true;
            shoot.enabled = true;
            tutorialText.text = "Space to shoot projectiles that can destroy the special orbs. Only these orbs can be destroyed.";

            spawnDestructibleEnemy = true;
        }
        //Set the spawner to spawn regular enemies & disable player shooting if not in the shootingTest state.
        else
        {
           //verticalEnemySpawner.spawnDestructible = false;
           shoot.enabled = false;

            spawnDestructibleEnemy = false;
        }

        //Enable the player's last-second dodging, change tutorial text, & enable the LSDVisualizer if in the LSDTest state.
        if(state == 2)
        {
            LSD.enabled = true;
            LSDVisualizer.SetActive(true);
            tutorialText.text = "Dodge orbs just before they touch you for a score bonus. The red rectangle shows the area an orb needs to be in for this bonus.";
            nextButton.interactable = false;
            exitButton.interactable = true;
        }
        //Disable the player's last-second dodging & the LSDVisualizer if not in the LSDTest state.
        else
        {
            LSD.enabled = false;
            LSDVisualizer.SetActive(false);
            nextButton.interactable = true;
        }
    }

    public void increaseState()
    {
        if(state < 2)
        {
            state++;
        }
    }

    public void decreaseState()
    {
        if (state > 0)
        {
            state--;
        }

    }
}
