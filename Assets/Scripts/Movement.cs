using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool secondP = false;
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
    public KeyCode jump;
    public KeyCode left;
    public KeyCode right;

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

        //Checks if chosen buttons for movement pressed
        if (Input.GetKey(right))
            moveInput = 1;
        else if (Input.GetKey(left))
            moveInput = -1;
        else
            moveInput = 0; 
        
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
        if (Input.GetKeyDown(jump) && isGrounded == true)   
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
