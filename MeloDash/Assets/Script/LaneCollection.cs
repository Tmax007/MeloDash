using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneCollection : MonoBehaviour
{

    //Set to 2 so player starts at the middle lane instead of the left (made public for logic of other GameObjects).

    public int currentLane = 2;

    public float movSpeed;
    float sinTime;

    bool moving;

    public Transform[] lanes;

    // Update is called once per frame
    void Update()
    {
        // Add boundary check before accessing the lanes array
        if (currentLane >= 0 && currentLane < lanes.Length && transform.position != lanes[currentLane].transform.position)
        {
            sinTime += Time.deltaTime * movSpeed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = evaluate(sinTime);

            //Interpolate to lane at index of the current lane integer.
            transform.position = Vector2.Lerp(transform.position, lanes[currentLane].transform.position, t);

            moving = true;
        }
        else
        {
            moving = false;
        }

        //If the player presses 'A' or the left arrow while not being at the left-most lane, they move left.
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentLane > 0)
        {
            sinTime = 0f;
            currentLane--;
        }

        //If the player presses 'D' or the right arrow while not being at the right-most lane, they move right.
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentLane < lanes.Length - 1)
        {
            sinTime = 0f;
            currentLane++;
        }

    }

    //Useful shit for movement interpolation
    public float evaluate(float x)
    {
        return (float)(0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5);
    }
}