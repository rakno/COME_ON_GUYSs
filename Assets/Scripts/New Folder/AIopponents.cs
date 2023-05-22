using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class AIopponents : MonoBehaviour
{
    
    public Transform[] waypoints; // Array to store the waypoints
    public float speed = 5f; // Speed of movement along the path
    public float waypointReachedThreshold = 0.1f; // Threshold distance to consider a waypoint as reached

    private int currentWaypointIndex = 0; // Current waypoint index
    private Animator animator; // Reference to the animator component

    private bool isFrozen = true; // Flag to track if the AI object is frozen
    private float delayDuration = 3f; // Delay duration before starting to follow waypoints

    private void Start()
    {

        // Get the animator component attached to the player model
        animator = GetComponent<Animator>();

        StartCoroutine(StartFollowingCoroutine());
    }

    private void Update()
    {
        if (isFrozen || currentWaypointIndex >= waypoints.Length)
        {
            // Stop the movement as the last waypoint has been reached or the object is frozen
            return;
        }

        // Move towards the current waypoint
        Transform currentWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);

        // Check if reached the current waypoint
        float distanceToWaypoint = Vector3.Distance(transform.position, currentWaypoint.position);
        Debug.Log("Distance to Waypoint " + currentWaypointIndex + ": " + distanceToWaypoint);

        if (distanceToWaypoint <= waypointReachedThreshold)
        {
            // Update the current waypoint index
            currentWaypointIndex++;

            // Check if reached the last waypoint
            if (currentWaypointIndex >= waypoints.Length)
            {
                // Stop the movement as the last waypoint has been reached
                currentWaypointIndex = waypoints.Length - 1;
            }
        }

        // Calculate the movement speed
        float movementSpeed = (transform.position - currentWaypoint.position).magnitude / Time.deltaTime;

        // Update the animator's "Speed" parameter based on the movement speed
        animator.SetFloat("Speed", movementSpeed);
    }

    IEnumerator StartFollowingCoroutine()
    {
        yield return new WaitForSeconds(delayDuration);
        isFrozen = false;
    }
}








