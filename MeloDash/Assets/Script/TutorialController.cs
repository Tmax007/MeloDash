using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    //Reference for the tutorial's enemy spawner.
    public VerticalEnemySpawner verticalEnemySpawner;

    //Reference for the player's last-second dodge script
    public LastSecondZone playerLastSecondDodge;

    //Enum storing the various stages of the tutorial.
    public enum tutorialState {dodgeTest, lastSecondDodgeTest, shootingTest}

    public tutorialState Tstate;

    public int state;

    // Start is called before the first frame update
    void Start()
    {
        //Spawns an enemy in the middle lane every 3 (as of now) seconds.
        verticalEnemySpawner.InvokeRepeating("SpawnVerticalEnemy", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        //Set the spawner to spawn destructible enemies if in the shootingTest state.
        if(Tstate == tutorialState.shootingTest)
        {
            //verticalEnemySpawner.spawnDestructible = true;
        }
        //Set the spawner to spawn regular enemies if not in the shootingTest state.
        else
        {
           // verticalEnemySpawner.spawnDestructible = false;
        }

        //Enable the player's last-second dodging if in the lastSecondDodgeTest state.
        if(Tstate == tutorialState.lastSecondDodgeTest)
        {
            playerLastSecondDodge.enabled = true;
            Debug.Log("A");
        }
        //Disable the player's last-second dodging if not in the lastSecondDodgeTest state.
        else
        {
            playerLastSecondDodge.enabled = false;
            Debug.Log("B");
        }
    }
}
