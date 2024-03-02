using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    public VerticalEnemySpawner verticalEnemySpawner;

    public LastSecondZone playerLastSecondDodge;

    //public int tutorialState = 0;

    public enum tutorialState {dodgeTest, lastSecondDodgeTest, shootingTest}

    public tutorialState state;

    // Start is called before the first frame update
    void Start()
    {
        verticalEnemySpawner.InvokeRepeating("SpawnVerticalEnemy", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {

        if(state == tutorialState.shootingTest)
        {
            verticalEnemySpawner.spawnDestructible = true;
        }
        else
        {
            verticalEnemySpawner.spawnDestructible = false;
        }

        if(state == tutorialState.lastSecondDodgeTest)
        {
            playerLastSecondDodge.enabled = true;
            Debug.Log("A");
        }
        else
        {
            playerLastSecondDodge.enabled = false;
            Debug.Log("B");
        }
    }
}
