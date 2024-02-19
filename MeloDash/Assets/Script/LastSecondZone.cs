using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSecondZone : MonoBehaviour
{

    //Floats adjust the size & vertical placement of the raycast.
    public float zoneX;
    public float zoneY;
    public float displacementY;

    LayerMask playerMask;
    LayerMask enemyMask;

    //Checks if an object with the "Player", "Enemy", or both layers are in the raycast.
    bool playerInMask = false;
    bool enemyInMask = false;
    bool bothInMask = false;

    //TEMPORARY DEBUG VARIABLE: Exists to communicate feedback for if the player has dodged at the last second, & which lane they did so from while the score system is still wonky.
    [SerializeField]
    int laneIndex;

    public ScoreManager scoreManager;
    public LaneCollection player;

    // Start is called before the first frame update
    void Start()
    {
        playerMask = LayerMask.NameToLayer("Player");
        enemyMask = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        //If the player presses 'A' or the left arrow while both an enemy & player are in the same zone & they aren't in the left most lane, they recieve a score bonus.
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && bothInMask && player.currentLane > 0)
        {
            //Current score bonus commented out due to not working right with the score system. Replaced with the game outputting the lane's laneIndex.
            //scoreManager.UpdateScore(10);
            Debug.Log(laneIndex);
        }

        //If the player presses 'D' or the right arrow while both an enemy & player are in the same zone & they aren't in the right most lane (will probably need to be changed manually for stuff like a tutorial), they recieve a score bonus.
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && bothInMask && player.currentLane < 4)
        {
            //Current score bonus commented out due to not working right with the score system. Replaced with the game outputting the lane's laneIndex.
            //scoreManager.UpdateScore(10);
            Debug.Log(laneIndex);
        }
    }

    private void FixedUpdate()
    {
        //Creates raycast.
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + displacementY), new Vector2(zoneX, zoneY), 0);

        //Checks if 2 or more objects are in the raycast at all times.
        if (colliders.Length >= 2)
        {
            //Goes through everything in the raycast to see if either has the "Player" or "Enemy" layer, enabling their respective booleans if they do. 
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.layer == playerMask)
                {
                    playerInMask = true;
                }

                if (colliders[i].gameObject.layer == enemyMask)
                {
                    enemyInMask = true;
                }
            }

            //If both booleans are true in this instance, enable the boolean stating so.
            if(playerInMask && enemyInMask)
            {
                bothInMask = true;
            }

        }

        //Sets all booleans to false if there are less than 2 objects found in the raycast (a bit messy but it works, might ask Keely or Jeff for help optimizing).
        else
        {
            playerInMask = false;
            enemyInMask = false;
            bothInMask = false;
        }

    }

    //Visualizes the position & proportion of the raycast in the editor for debugging. These are invisible during actual play.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + displacementY), new Vector3(zoneX, zoneY, 0));
    }
}
