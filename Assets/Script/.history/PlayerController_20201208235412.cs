using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coli;
    private AudioController audioControl;
    [SerializeField] private LayerMask ground;
    public GameObject gateSprite;
    [SerializeField] private LayerMask gateLayer;
    [SerializeField] private float hurtForce = 10f;
    public GameObject gatePos;
    public int numSpawnGen;
    
    private enum State { idle, running, jumping, falling, hurt };
    private State state = State.idle;
    public int cherries = 0 ;
    public int gems = 0;
    public int hurts = 0;

    public Text cherryText;
    public Text gemText;
    public Text hurtText;
    public GameObject gameOver;
    public GameObject dialogInfo;
    private SpwanObjectRandom spwanObjectRandom;
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Collectable"){
            Destroy(collision.gameObject);
            cherries+=1;
        }
        if(collision.tag == "Gem"){
            Destroy(collision.gameObject);
            gems+=1;
        }
    }

    private void Start()
    {
        audioControl = GetComponent<AudioController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coli = GetComponent<Collider2D>();
        spwanObjectRandom = GetComponent<SpwanObjectRandom>();
        for (int i = 0; i < numSpawnGen; i++) 
        {
            spwanObjectRandom.SpawnThing();
        }
        cherryText = dialogInfo.transform.Find("CherryNum").GetComponent<Text>();
        gemText = dialogInfo.transform.Find("GemNum").GetComponent<Text>();
        hurtText = dialogInfo.transform.Find("HurtNum").GetComponent<Text>();

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
            Jump();
        }
        
        if(Vector2.Distance(transform.position, gatePos.transform.position)<=1){

            cherryText.text = "" + cherries;
            gemText.text = "" + gems;
            hurtText.text = "" + hurts;
            gateSprite.SetActive(true);
        }
        if(coli.IsTouchingLayers(gateLayer)){
            dialogInfo.SetActive(true);
        }
        if(transform.position.y < -10f){
            rb.gravityScale =0;
            gameOver.SetActive(true);
        }
    }

    private void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, 10f);
        state = State.jumping;
        audioControl.PlayJump();
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
            if(state == State.falling){
                 audioControl.PlayAttack();
                Destroy(other.gameObject);
                Jump();
            }
            else{
                state = State.hurt;
                hurts++;
                // anim.SetInteger("state", (int)state);
                if(other.gameObject.transform.position.x > transform.position.x){
                    rb.velocity = new Vector2(-hurtForce,rb.velocity.y);
                }
                else{
                    rb.velocity = new Vector2(hurtForce,rb.velocity.y);
                }
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
        else if (state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) <.1f)
            {
                state = State.idle;
            }
            if (hurts>=10)
            {
                gameOver.SetActive(true);
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
