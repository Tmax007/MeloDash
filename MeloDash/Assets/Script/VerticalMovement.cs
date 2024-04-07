using UnityEngine;
using System.Collections;

public class VerticalMovement : MonoBehaviour
{
    public float initialSpeed = 2.0f; // Initial speed of vertical movement
    public float topBound = 5.0f; // Upper bound of movement
    public float bottomBound = -5.0f; // Lower bound of movement
    public float speedIncrement = 0.1f; // Speed increment value
    public float speedIncrementInterval = 5.0f; // Interval for speed increment

    private float currentSpeed; // Current speed of vertical movement

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = initialSpeed;
        StartCoroutine(IncreaseSpeedRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the movement amount based on speed and time
        float movement = currentSpeed * Time.deltaTime;

        // Move the object vertically
        transform.Translate(Vector3.right * movement);

        // Check if the object has reached the top bound
        if (transform.position.y >= topBound)
        {
            // Calculate the remaining distance to move to the bottom bound
            float remainingDistance = topBound - transform.position.y;

            // Teleport the object to the bottom bound
            transform.position = new Vector3(transform.position.x, bottomBound - remainingDistance, transform.position.z);
        }
    }

    // Coroutine to gradually increase speed over time
    IEnumerator IncreaseSpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncrementInterval);
            currentSpeed += speedIncrement;
        }
    }
}