using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 destination;
    private float descentSpeed;

    private bool reachedSnapPoint = false;

    // Set destination point for enemy to move towards
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }

    // Sets descent speed of enemy
    public void SetDescentSpeed(float speed)
    {
        descentSpeed = speed;
    }

    void Update()
    {
        // If enemy hasn't reached snap point yet
        if (!reachedSnapPoint)
        {
            // Move towards destination point
            transform.position = Vector3.MoveTowards(transform.position, destination, descentSpeed * Time.deltaTime);

            // Check if enemy has reached the destination
            if (transform.position == destination)
            {
                reachedSnapPoint = true;
            }
        }
        else
        {
            // Move downward until off-screen
            transform.Translate(Vector3.down * descentSpeed * Time.deltaTime);

            // Check if enemy is off-screen
            if (!IsVisibleFrom(Camera.main))
            {
                // Destroy enemy if it's off-screen
                Destroy(gameObject);
            }
        }
    }

    // Checks if enemy is visible from camera
    bool IsVisibleFrom(Camera camera)
    {
        // Calculate frustum planes of the camera
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

        // Check if enemy's bounding box intersects with any of the camera's frustum planes
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds);
    }
}
