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
    private enum State { idle, running, jumping, falling };
    private State state = State.idle;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coli = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if(hDirection <0 )
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }
        else
        {
           // anim.SetBool("running", false);
        }

        if (Input.GetButtonDown("Jump") && coli.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
            state = State.jumping;
        }

        VelocitySwitch();
        anim.SetInteger("state", (int)state);
    }

    private void VelocitySwitch()
    {
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
