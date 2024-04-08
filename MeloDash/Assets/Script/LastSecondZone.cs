using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSecondZone : MonoBehaviour
{
    // Floats adjust the size & vertical placement of the raycast.
    public float zoneX;
    public float zoneY;
    public float displacementY;

    private LayerMask enemyMask;
    private LaneCollection controls;

    [HideInInspector]
    public int dodgeNum = 0;
    private bool enemyInCast = false;
    public ScoreManager scoreManager;
    public AudioSource[] LSDSound;

    // Camera shake variables
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
        controls = gameObject.GetComponent<LaneCollection>();
        LSDSound = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player moves left or right while enemyInCast is true, they gain a score bonus of 10.
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && controls.currentLane > 0 && enemyInCast == true)
        {
            DodgeSuccess();
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && controls.currentLane < controls.lanes.Length - 1 && enemyInCast == true)
        {
            DodgeSuccess();
        }
    }

    private void FixedUpdate()
    {
        // Generates a raycast around the player that looks for objects with the "Enemy" layer. 
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector3(transform.position.x, transform.position.y + displacementY), new Vector3(zoneX, zoneY), 0, enemyMask);

        // If it detects something, enemyInCast is set to true. If it doesn't it's set to false.
        if (colliders.Length > 0)
        {
            enemyInCast = true;
        }
        else
        {
            enemyInCast = false;
        }
    }

    // Visualizes the position & proportion of the raycast in the editor for debugging. These are invisible during actual play.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + displacementY), new Vector3(zoneX, zoneY, 0));
    }

    // Method to handle successful dodge
    private void DodgeSuccess()
    {
        // Play sound effect
        LSDSound[1].Play();

        // Trigger camera shake
        ShakeCamera();

        // Update score and dodge count
        scoreManager.UpdateScore(10);
        dodgeNum++;
    }

    // Camera shake method
    void ShakeCamera()
    {
        StartCoroutine(Shake(mainCamera.transform, shakeDuration, shakeMagnitude));
    }

    IEnumerator Shake(Transform camTransform, float duration, float magnitude)
    {
        Vector3 originalPos = camTransform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            camTransform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        camTransform.localPosition = originalPos;
    }
}