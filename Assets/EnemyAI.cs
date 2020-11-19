using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Path path;
    int currentWaypopint = 0;
    bool reachedEndOfPath = false;
    public Seeker seeker;
    Rigidbody2D rb;


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(rb.position, target.position, OnPathComplete);   
    }

    void OnPathComplete(Path p){
        if (!p.error)
        {
            path = p;
            currentWaypopint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
