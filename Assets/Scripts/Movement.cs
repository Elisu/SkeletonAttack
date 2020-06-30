using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;                     
    public float jumpForce;              
    private float moveInput;
    private Rigidbody2D rb;
    public SpriteRenderer sr;         

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;              

    Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerBody = transform.Find("PlayerBody");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //checks wheter player is touching ground
       isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround); 

        
        moveInput = Input.GetAxisRaw("Horizontal");    
        
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);  

        if (facingRight == false && moveInput > 0)  
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)   
        {
            //jump
            rb.velocity = Vector2.up * jumpForce;      
        }
        
        
    }

    
    //flips character sprite to face dircetion of movement
    void Flip()                                     
    {                                               
        facingRight = !facingRight;
        Vector3 Scaler = playerBody.localScale;
        Scaler.x *= -1;
        playerBody.localScale = Scaler;
    }
}
