using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanObjectRandom : MonoBehaviour
{
    public float startDistance;
    public float endDistance;
    public int groundCheckStepCount = 5; // how many intermediate raycasts to do
    public Transform playerTransform; // location of the player (or thing doing the spawning)
    public float rayLength = 3f; // distance downward to extend the ray
    public LayerMask layersToHit; // layers to hit (set to ground layer probably)
    public GameObject thingToSpawn; // object to spawn
 
    /// <summary>
    /// Spawns the thing using the special raycasting logic
    /// </summary>
    public void SpawnThing()
    {
        Vector3 spawnLocation;
        if(GetSpawnLocation(out spawnLocation, towardsPlayerFromMaxDistance: true))
        {
            Instantiate(thingToSpawn, spawnLocation, Quaternion.identity);
        }
    }
 
    /// <summary>
    /// Gets the spawn location. Returns true if it found a location, and puts the position in "spawnLocation".
    /// use "towardsPlayerFromMaxDistance" to control the direction of raycast checks.
    /// </summary>
    public bool GetSpawnLocation(out Vector3 spawnLocation, bool towardsPlayerFromMaxDistance)
    {
        int raycastCount = groundCheckStepCount + 2; // beginning & end & steps in between
 
        // do all the raycasts
        for (int i = 0; i <= raycastCount; i++)
        {
            float percentage =  (float)i / raycastCount;
            float interpolationValue = towardsPlayerFromMaxDistance ? (1 - percentage) : percentage; 
            // invert percentage if necessary
 
            // interpolate between min/max distance
            float spawnDistance = Random.Range(startDistance, endDistance);
            float distance = Mathf.Lerp(0, spawnDistance, interpolationValue);
            RaycastHit2D hit2D = RaycastDownAtDistance(distance);
 
            // if it hit something
            if(hit2D.collider != null)
            {
                // assign the valid location and return true
                spawnLocation = hit2D.point;
                return true;
            }
        }
 
        // got thru all loops without returning, so assign zero and return false
        spawnLocation = Vector3.zero;
        return false;
    }
 
    /// <summary>
    /// Raycasts downward at the given distance, returns the hit result
    /// </summary>
    public RaycastHit2D RaycastDownAtDistance(float distance)
    {
        Vector3 origin = playerTransform.position + new Vector3(distance, 1);
        Ray ray = new Ray(origin, Vector3.down);
        return Physics2D.Raycast(ray.origin, ray.direction, rayLength, layersToHit);
    }

    
}