using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumController : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    bool facingLeft = false;
    private float movementSpeed = .085f;
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
            facingLeft = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        WallCollsionHandler(collision);
    }

    void UpdatePosition(){
        Vector3 oPos = transform.position;
        float calculatedPosition;
        if(facingLeft){
            transform.localScale = new Vector2(-1, 1);
            calculatedPosition = oPos.x + movementSpeed;
        } else {
            transform.localScale = new Vector2(1, 1);
            calculatedPosition = oPos.x - movementSpeed;
        }
        transform.position = new Vector3(calculatedPosition, oPos.y, oPos.z);   
    }
    void FixedUpdate()
    {
        UpdatePosition();
    }
}
