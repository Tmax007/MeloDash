using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LastSecondZone : MonoBehaviour
{

    //Floats adjust the size & vertical placement of the raycast.
    public float zoneX;
    public float zoneY;
    public float displacementY;

    LayerMask enemyMask;
    LaneCollection controls;

    //Checks if an object with the "Player", "Enemy", or both layers are in the raycast.
    bool enemyInCast = false;

    public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
        controls = gameObject.GetComponent<LaneCollection>();
        //Debug.Log(enemyMask);
    }

    // Update is called once per frame
    void Update()
    {
        //If the player moves left or right while enemyInCast is true, they gain a score bonus of 10.
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && controls.currentLane > 0 && enemyInCast == true)
        {
            scoreManager.UpdateScore(10);
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && controls.currentLane < controls.lanes.Length - 1 && enemyInCast == true)
        {
            scoreManager.UpdateScore(10);
        }
    }

    private void FixedUpdate()
    {
        //Generates a raycast around the player that looks for objects with the "Enemy" layer. 
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector3(transform.position.x, transform.position.y + displacementY), new Vector3(zoneX, zoneY), 0, enemyMask);

        //If it detects something, enemyInCast is set to true. If it doesn't it's set to false.
        if (colliders.Length > 0)
        {
            enemyInCast = true;
        }
        else
        {
            enemyInCast = false;
        }

        //Debug.Log(colliders.Length);
    }

    //Visualizes the position & proportion of the raycast in the editor for debugging. These are invisible during actual play.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + displacementY), new Vector3(zoneX, zoneY, 0));
    }
}
