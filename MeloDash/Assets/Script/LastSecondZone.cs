using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSecondZone : MonoBehaviour
{

    public float zoneX;
    public float zoneY;
    public float displacementY;

    LayerMask playerMask;
    LayerMask enemyMask;

    bool playerInMask = false;
    bool enemyInMask = false;
    bool bothInMask = false;

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
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && bothInMask && player.currentLane > 0)
        {
            //scoreManager.UpdateScore(10);
            Debug.Log(laneIndex);
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && bothInMask && player.currentLane < 4)
        {
            //scoreManager.UpdateScore(10);
            Debug.Log(laneIndex);
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + displacementY), new Vector2(zoneX, zoneY), 0);

        if (colliders.Length >= 2)
        {
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

            if(playerInMask && enemyInMask)
            {
                bothInMask = true;
            }

        }

        else
        {
            playerInMask = false;
            enemyInMask = false;
            bothInMask = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + displacementY), new Vector3(zoneX, zoneY, 0));
    }
}
