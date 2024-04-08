using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 destination;
    private float descentSpeed;
    private bool reachedSnapPoint = false;

    public ScoreManager scoreManager;
    public int endReachedPointValue;
    public int destructiblePointValue;

    public GameObject destructionAnimationPrefab; 
    
    //public TrailRenderer trailRenderer; // Reference to the Trail Renderer component

    void Start()
    {
        // Find the ScoreManager GameObject and get its ScoreManager component
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

        /* Disable the Trail Renderer initially
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }*/
    }

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

            /*// Enable the Trail Renderer when the enemy starts moving
            if (trailRenderer != null && !trailRenderer.enabled)
            {
                trailRenderer.enabled = true;
            }*/

            // Check if enemy has reached the destination
            if (transform.position == destination)
            {
                reachedSnapPoint = true;

                // Play destruction animation
                if (destructionAnimationPrefab != null)
                {
                    var copy= Instantiate(destructionAnimationPrefab, transform.position, Quaternion.identity);
                    // Destroy destruction animation after a delay
                    Destroy(copy, 2f);
                }

                // Destroy the enemy GameObject
                Destroy(gameObject);
            }
        }
    }

    // Checks if enemy is visible from camera
    public bool IsVisibleFrom(Camera camera)
    {
        // Calculate frustum planes of the camera
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

        // Check if enemy's bounding box intersects with any of the camera's frustum planes
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds);
    }

    private void OnDestroy()
    {
        if (scoreManager != null)
        {
            if (reachedSnapPoint)
            {
                // Update the score when the enemy reaches the snap point
                scoreManager.UpdateScore(endReachedPointValue);
            }
            else if (!reachedSnapPoint)
            {
                scoreManager.UpdateScore(destructiblePointValue);
            }
        }
    }
}