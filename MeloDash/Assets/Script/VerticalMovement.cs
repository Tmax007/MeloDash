using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    public float speed = 2.0f; // Speed of vertical movement
    public float topBound = 5.0f; // Upper bound of movement
    public float bottomBound = -5.0f; // Lower bound of movement

    // Update is called once per frame
    void Update()
    {
        // Calculate the movement amount based on speed and time
        float movement = speed * Time.deltaTime;

        // Move the object vertically
        transform.Translate(Vector3.right * movement);

        // Check if the object has reached the top bound
        if (transform.position.y >= topBound)
        {
            // Teleport the object to the bottom bound to create continuous vertical movement
            transform.position = new Vector3(transform.position.x, bottomBound, transform.position.z);
        }
    }
}
