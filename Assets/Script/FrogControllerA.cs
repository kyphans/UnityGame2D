using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogControllerA : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float jumpLength = 10f;
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    bool facingLeft = false;
    private float movementSpeed = .15f;
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void WallCollsionHandler(Collider2D coll){
        if(coll.gameObject.tag == "Wall Left")
        {
            facingLeft = true;
        }
        else if(coll.gameObject.tag == "Wall Right")
        {
            Debug.Log("Hit Wall Right");
            facingLeft = false;
        }
    }

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     WallCollsionHandler(col);
    // }

    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("OnTrigger!");
        WallCollsionHandler(collision);
    }

    void UpdatePosition(){
        Vector3 oPos = transform.position;
        float calculatedPosition;
        if(facingLeft){
            if (transform.localScale.x != 1)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            if (coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(-jumpLength, jumpLength);
                // anim.SetBool("Jumping",true);
            }
            calculatedPosition = oPos.x + movementSpeed;
            transform.position = new Vector3(calculatedPosition, oPos.y, oPos.z);
        } else {
            
            if (transform.localScale.x != 1)
            {
                transform.localScale = new Vector2(1, 1);
            }
            if (coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(jumpLength, jumpLength);
                // anim.SetBool("Jumping",true);
            }
            calculatedPosition = oPos.x - movementSpeed;
            transform.position = new Vector3(calculatedPosition, oPos.y, oPos.z);
        }

        
    }
    void SetUpAnimation(){
        if(anim.GetBool("Jumping")){
            if(rb.velocity.y < .1){
                // Debug.Log("rb.velocity.y < .1");
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
        Debug.Log("Jump:"+anim.GetBool("Jumping")+"/"+"Falling:"+anim.GetBool("Falling"));
    }

    void FixedUpdate()
    {
        UpdatePosition();
        // SetUpAnimation();
    }

    
}
