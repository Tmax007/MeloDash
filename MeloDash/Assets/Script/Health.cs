using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int healthNum;
    int maxHealth;

    public string damageSourceLayer;

    public bool regenHealth;
    bool regenerating;

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

        //Debug.Log(collision.gameObject.layer);

        if (collision.gameObject.layer == LayerMask.NameToLayer(damageSourceLayer))
        {
            Destroy(collision.gameObject);

            healthNum--;

            regenerating = true;

        }

    }

    void healthIncrease()
    {
        healthNum++;
    }

    void healthRegeneration()
    {
        currentTime += Time.deltaTime;
        
        //Debug.Log(currentTime);

        if (currentTime >= regenTime) 
        {
            currentTime = 0;
            healthIncrease(); 

        }

        if(healthNum == maxHealth)
        {
            currentTime = 0;
            regenerating = false;
        }
    }
}
