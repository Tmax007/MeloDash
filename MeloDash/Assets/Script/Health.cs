using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    LastSecondZone LSD;

    Shooting playerShoots;

    AudioSource[] damageSound;

    Animator playerAnimator;
    public AnimationClip hurtAnimation; 


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = healthNum;
        regenerating = false;

        // Get the Animator component of the player GameObject
        playerAnimator = GetComponent<Animator>();

        if (gameObject.name == "Player")
        {
            playerControls = gameObject.GetComponent<LaneCollection>();
            LSD = gameObject.GetComponent<LastSecondZone>();
            playerShoots = gameObject.GetComponent<Shooting>();

            if(SceneManager.GetActiveScene().name == "MainGame")
            {
                Init.run++;

                //Sets the player to die after 125 seconds so that data can still be logged in winning attempts during testing.
                //Being set to occur after 125 seconds means that it can only happen after the player's gotten through everything.
                Invoke("selfDestruct", 125);
            }

            TelemetryLogger.Log(this, "Start game.");
        }

        damageSound = GetComponents<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(healthNum == 0)
        {
            Destroy(gameObject, 0.1f);

            if (gameObject.name == "Player")
            {

                var data = new DeathEventData()
                {
                    secondsIntoLevel = (int)Time.timeSinceLevelLoad + 1,
                    lane = playerControls.currentLane,
                    finalScore = Init.score,
                    LastSecondDodges = LSD.dodgeNum,
                    enemiesDestroyed = playerShoots.enemiesDestroyed,
                    runNumber = Init.run
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

            damageSound[0].Play();

            if (gameObject.name == "Player" && healthNum > 0)
            {
                var data = new DamageEventData()
                {
                    secondsIntoLevel = (int)Time.timeSinceLevelLoad + 1,
                    lane = playerControls.currentLane,
                    runNumber = Init.run
                };

                TelemetryLogger.Log(this, "PlayerDamage", data);
            }
            // Play the hurt animation directly
            playerAnimator.Play(hurtAnimation.name);
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
    public struct DamageEventData
    {
        public int secondsIntoLevel;

        public int lane;

        public int runNumber;
    }

    [System.Serializable]
    public struct DeathEventData
    {
        public int secondsIntoLevel;

        public int lane;

        public int finalScore;

        public int LastSecondDodges;

        public int enemiesDestroyed;

        public int runNumber;

    }

    //This exists solely for telemetry reasons, so that we can get data from players who make it to the end.
    void selfDestruct()
    {
        healthNum = 0;
    }

}
