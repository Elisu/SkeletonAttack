using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;                     //rychlost běhu postavy
    public float jumpForce;                 //síla skoku
    private float moveInput;
    private Rigidbody2D rb;
    public SpriteRenderer sr;           //obrázek postavy

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;              //na určení co se bere jako země

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
       isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);  //kontrola, zda postava stojí nebo ve vzduchu

        
        moveInput = Input.GetAxisRaw("Horizontal");      //vstup od hráče
        
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);   //výpočet rychlosti, pouze na x, na y se nezmění, tedy pohyb doprava a doleva

        if (facingRight == false && moveInput > 0)  //kontrola otočení obrázku
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
            rb.velocity = Vector2.up * jumpForce;      //skok, když zmáčknut mezerník a postava na zemi
        }
        
        
    }

    

    void Flip()                                     //otočení obrázku postavy
    {                                               
        facingRight = !facingRight;
        Vector3 Scaler = playerBody.localScale;
        Scaler.x *= -1;
        playerBody.localScale = Scaler;
    }
}
