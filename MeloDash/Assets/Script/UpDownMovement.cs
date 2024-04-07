using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    public float moveDistance = 2f; // Distance to move up and down
    public float moveSpeed = 2f; // Speed of the movement

    private Vector3 startPosition; // Initial position of the object

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the vertical movement using a sine wave
        float verticalMovement = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        // Update the position of the object
        transform.position = startPosition + Vector3.up * verticalMovement;
    }
}
