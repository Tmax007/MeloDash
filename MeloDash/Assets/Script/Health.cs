using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int healthNum;
    int maxHealth;

    public LayerMask damageSourceLayer;

    //regenHealth is a public boolean that can be toggled in the inspector to enable/disable regenerating health (should be enabled for stuff we want to regenerate health like the player, & disabled for what we don't want like destructible enemies).
    public bool regenHealth;
    //regenerating is used for script logic to determine when entity is currently regenerating health.
    bool regenerating;

    //regenTime determines the length of time needed for an entity to regenerate 1 health.
    public float regenTime;
    [HideInInspector]  
    public float currentTime = 0;

    LaneCollection playerControls;

    public HealthUIUpdate healthUI;

    public ScoreManager scoreManager;

    AudioSource damageSound;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = healthNum;
        regenerating = false;

        if (gameObject.name == "Player")
        {
            playerControls = gameObject.GetComponent<LaneCollection>();

            //Sets the player to die after 125 seconds so that data can still be logged in winning attempts during testing.
            //Being set to occur after 125 seconds means that it can only happen after the player's gotten through everything.
            //Invoke("selfDestruct", 125);

            TelemetryLogger.Log(this, "Start game.");
        }

        damageSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthNum == 0)
        {
            Destroy(gameObject);

            if (gameObject.name == "Player")
            {

                var data = new DamageDeathEventData()
                {
                    secondsIntoLevel = (int)Time.timeSinceLevelLoad + 1,
                    lane = playerControls.currentLane,
                    finalScore = scoreManager.score
                };

                TelemetryLogger.Log(this, "PlayerDeath", data);
            }
        }

        if(gameObject.name == "Player")
        {
            //Debug.Log((int)Time.timeSinceLevelLoad + 1);
        }

        if(regenerating && regenHealth)
        {
            healthRegeneration();
        }

        healthUI.SendMessage("UpdateUINumber");

        //Debug.Log(regenerating);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is on the damageSourceLayer
        if (damageSourceLayer == (damageSourceLayer | (1 << collision.gameObject.layer)))
        {
            Destroy(collision.gameObject);
            healthNum--;
            currentTime = 0;
            regenerating = true;

            if (gameObject.name == "Player" && healthNum > 0)
            {
                damageSound.Play();

                var data = new DamageDeathEventData()
                {
                    secondsIntoLevel = (int)Time.timeSinceLevelLoad + 1,
                    lane = playerControls.currentLane
                };

                TelemetryLogger.Log(this, "PlayerDamage", data);
            }    
        }
    }

    void healthRegeneration()
    {
        currentTime += Time.deltaTime;
        
        //Debug.Log(currentTime);

        if (currentTime >= regenTime) 
        {
            currentTime = 0;
            healthNum++;
        }

        if(healthNum == maxHealth)
        {
            currentTime = 0;
            regenerating = false;
        }
    }

    [System.Serializable]
    public struct DamageDeathEventData
    {
        public int secondsIntoLevel;

        public int lane;

        public int finalScore;
    }

    //This exists solely for telemetry reasons, so that we can get data from players who make it to the end.
    void selfDestruct()
    {
        healthNum = 0;
    }

}
