using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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

    public Transform enemyGFX;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath",0f,0.5f);
        seeker.StartPath(rb.position, target.position, OnPathComplete);   
    }
    void UpdatePath(){
        if(seeker.IsDone()){
            seeker.StartPath(rb.position,target.position,OnPathComplete);
        }
    }

    void OnPathComplete(Path p){
        if (!p.error)
        {
            path = p;
            currentWaypopint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) return;
        
        if (currentWaypopint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
        }
        else{
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypopint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypopint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypopint++;
        }
        if (force.x>=0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f,1f,1f);
        }
        else if(force.x <= -0.01f){
            enemyGFX.localScale = new Vector3(1f,1f,1f);
        }
    }
}
