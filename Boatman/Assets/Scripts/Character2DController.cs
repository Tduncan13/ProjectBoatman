using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    Animator anim;
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public bool isRunning;
    private bool turn;
    private bool facingFront = true;
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    // Wall Jump/Slide Variables
    public bool isTouchingFront;
    public Transform frontCheck;
    public bool wallSliding;
    public float wallSlideSpeed;
    public float wallJumpForceX;
    public float wallJumpForceY;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        
        // Horizontal character movement controller.
        var movement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movement * MovementSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        // Set animation boolean for running animation. 
        isRunning = (movement != 0); 
        anim.SetBool("isRunning", isRunning);

        // Check character movement to get character orientation. 
        turn = (movement < 0 && facingFront) ^ (movement > 0 && !facingFront);
        if(turn){
            flip();
        } 

        // Character jump controller
        if(Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        if (isTouchingFront == true && isGrounded == false && movement != 0){
            wallSliding = true;
        } else {
            wallSliding = false;
        }

        if(wallSliding){
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
    }

    void flip(){
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingFront = !facingFront;
    }
}
