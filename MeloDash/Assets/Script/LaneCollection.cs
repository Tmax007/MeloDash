using UnityEngine;

public class LaneCollection : MonoBehaviour
{
    public int currentLane = 2;
    public float movSpeed;
    float sinTime;
    bool moving;

    public Transform[] lanes;
    public Animator animator;
    private string moveLeftParam = "MoveLeft";
    private string moveRightParam = "MoveRight"; 
    private string idleParam = "Idle"; 

    // Update is called once per frame
    void Update()
    {
        // If the player is not moving, set the idle parameter to true
        animator.SetBool(idleParam, !moving);

        if (currentLane >= 0 && currentLane < lanes.Length && transform.position != lanes[currentLane].transform.position)
        {
            sinTime += Time.deltaTime * movSpeed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = evaluate(sinTime);
            transform.position = Vector2.Lerp(transform.position, lanes[currentLane].transform.position, t);

            // If the player is moving, ensure the idle parameter is false
            moving = true;
        }
        else
        {
            moving = false;
        }

        // If the player presses 'A' or the left arrow while not being at the left-most lane, they move left.
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentLane > 0)
        {
            sinTime = 0f;
            currentLane--;

            // Set the boolean parameter for moving left
            animator.SetBool(moveLeftParam, true);
            animator.SetBool(moveRightParam, false); 
            animator.SetBool(idleParam, false); 
                                                
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Left"))
            {
                animator.Play("Player_Left");
            }

            // Reset the right movement parameter
            animator.SetBool(moveLeftParam, false);
        }

        // If the player presses 'D' or the right arrow while not being at the right-most lane, they move right.
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentLane < lanes.Length - 1)
        {
            sinTime = 0f;
            currentLane++;

            // Set the boolean parameter for moving right
            animator.SetBool(moveRightParam, true);
            animator.SetBool(moveLeftParam, false); 
            animator.SetBool(idleParam, false); 
                                             
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Right"))
            {
                animator.Play("Player_Right");
            }

            // Reset the left movement parameter
            animator.SetBool(moveRightParam, false);
        }

    }

    // Useful function for movement interpolation
    public float evaluate(float x)
    {
        return (float)(0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5);
    }
}
