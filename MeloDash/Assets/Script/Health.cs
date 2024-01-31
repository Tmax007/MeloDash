using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int healthNum;

    public string damageSourceLayer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(healthNum == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log(collision.gameObject.layer);

        if (collision.gameObject.layer == LayerMask.NameToLayer(damageSourceLayer))
        {
            Destroy(collision.gameObject);

            healthNum--;

        }

    }

    void healthIncrease()
    {
        healthNum++;
    }
}
