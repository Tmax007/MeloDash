using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int healthNum;
    int maxHealth;

    public LayerMask damageSourceLayer;

    //regenHealth is a public boolean that can be toggled in the inspector to enable/disable regenerating health.
    public bool regenHealth;
    //regenerating is used for script logic to determine when entity is currently regenerating health.
    bool regenerating;

    //regenTime determines the length of time needed for an entity to regenerate 1 health.
    public float regenTime;
    float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = healthNum;
        regenerating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthNum == 0)
        {
            Destroy(gameObject);
        }

        if(regenerating && regenHealth)
        {
            healthRegeneration();
        }

        //Debug.Log(regenerating);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is on the damageSourceLayer
        if (damageSourceLayer == (damageSourceLayer | (1 << collision.gameObject.layer)))
        {
            Destroy(collision.gameObject);
            healthNum--;
            regenerating = true;
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
}
