using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 10f;
    private Transform target;
    private int waypointIndex;

    private void Start()
    {
        target = Waypoints.waypoints[0];
        waypointIndex = 0;
    }

    private void Update()
    {
        // Update the target if it reaches the current waypoint
        if (Vector3.Distance(target.position, transform.position) <= 0.2f)
        {
            waypointIndex++;
            // If the enemy reaches the end point, destroy the enemy
            if (waypointIndex == Waypoints.waypoints.Length)
            {
                Destroy(gameObject);
                return;
            }
            target = Waypoints.waypoints[waypointIndex];
        }
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        
    }
}
