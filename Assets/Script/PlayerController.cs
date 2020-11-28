using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coli;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float hurtForce = 10f;
    
    private enum State { idle, running, jumping, falling, hurt };
    private State state = State.idle;
    public int cherries = 0 ;
    
    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Trigger: collider2D");
        if(collision.tag == "Collectable"){
            Destroy(collision.gameObject);
            cherries+=1;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coli = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Movement(){
        float hDirection = Input.GetAxis("Horizontal");

        if(hDirection < 0 )
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }

        if (Input.GetButtonDown("Jump") && coli.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
            state = State.jumping;
        }
    }
    private void Update()
    {
        if(state!=State.hurt){
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag=="Enemy"){
            Destroy(other.gameObject);
        }
        else{
            state = State.hurt;
            if(other.gameObject.transform.position.x > transform.position.x){
                rb.velocity = new Vector2(-hurtForce,rb.velocity.y);
            }
            else{
                rb.velocity = new Vector2(hurtForce,rb.velocity.y);
            }
        }
    }

    private void AnimationState(){
        if (state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }

        else if (state == State.falling)
        {
            if (coli.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x)  > 2f)
        {
            state = State.running;
        }
        
        else
        {
            state = State.idle;
        }
    }
}
