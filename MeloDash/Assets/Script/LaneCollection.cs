using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneCollection : MonoBehaviour
{

    int currentLane = 0;

    public float movSpeed;
    float sinTime;

    bool moving;

    public Transform[] lanes;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Add boundary check before accessing the lanes array
        if (currentLane >= 0 && currentLane < lanes.Length && transform.position != lanes[currentLane].transform.position)
        {
            sinTime += Time.deltaTime * movSpeed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = evaluate(sinTime);

            transform.position = Vector2.Lerp(transform.position, lanes[currentLane].transform.position, t);

            moving = true;
        }
        else
        {
            moving = false;
        }

        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0 && moving == false)
        {
            sinTime = 0f;
            currentLane--;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && currentLane < lanes.Length - 1 && moving == false)
        {
            sinTime = 0f;
            currentLane++;
        }

        //Debug.Log((transform.position == lanes[currentLane].transform.position));
    }

    public float evaluate(float x)
    {
        return (float)(0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5);
    }
}
