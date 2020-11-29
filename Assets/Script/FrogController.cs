using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    private Animator anim;

    private Rigidbody2D rb;
    private bool facingLeft = true;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    private Collider2D coll;

    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Move(){
        if (facingLeft)
            {
                if (transform.position.x > leftCap)
                {
                    if (transform.localScale.x != 1)
                    {
                        transform.localScale = new Vector3(1,1);
                    }

                    if (coll.IsTouchingLayers(ground));
                    {
                        rb.velocity = new Vector2(-jumpLength, jumpLength);
                        anim.SetBool("Jumping",true);
                    }
                }
                else{
                    facingLeft = false;
                }
            }
            else{
                if (transform.position.x < rightCap)
                {
                    if (transform.localScale.x != 1)
                    {
                        transform.localScale = new Vector3(-1,1);
                    }

                    if (coll.IsTouchingLayers(ground));
                    {
                        rb.velocity = new Vector2(jumpLength, jumpLength);
                        anim.SetBool("Jumping",true);
                    }
                }
                else{
                    facingLeft = false;
                }
            }
        }
    void Update()
    {
        if(anim.GetBool("Jumping")){
            if(rb.velocity.y < .1){
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }
}
