using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem destructionParticles;
    private Vector3 destination;
    private float descentSpeed;

    private bool reachedSnapPoint = false;

    public ScoreManager scoreManager;

    public int endReachedPointValue;
    public int destructiblePointValue;

    AudioSource destructionSound;

    void Start()
    {
        // Find the ScoreManager GameObject and get its ScoreManager component
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

        destructionParticles = GameObject.FindObjectOfType<ParticleSystem>();

        //Debug.Log(gameObject.layer);
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

            // Check if enemy has reached the destination
            if (transform.position == destination)
            {
                reachedSnapPoint = true;

                // Trigger particle system
                if (destructionParticles != null)
                {
                    // Set the position of the particle system to the enemy's position
                    destructionParticles.transform.position = transform.position;
                    destructionParticles.Play();
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
            if(reachedSnapPoint)
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
