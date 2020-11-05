using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
<<<<<<< HEAD
    //  TODO:   
    //          [] Finish Wall Jumping
    //          [] Implement Wall sliding on select walls
    //          [] Implement Particle System for Running
    //          [] Implement Partical System for Landing
    //          [] Implement Partical System for Jumping

    private Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer spriteRenderer;

    // Camera Tartget Variables
    public Transform cameraTarget;
    public float aheadAmount, aheadSpeed;

    // Player Movement Variables
    public float movementSpeed = 1, jumpForce = 1, checkRadius, jumpBufferLength, jumpTime, jumpTimeCount;
    private float jumpBufferCount;
    public bool isRunning, isGrounded, isJumping;
=======
    Animator anim;
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public bool isRunning;
    private bool turn;
    private bool facingFront = true;
    public bool isGrounded;
>>>>>>> parent of dad1ca8... Dynamic Jumping Added. Dynamic Camera System Added. Hang Time Implemented. Jump Buffer Implemented. Wall Sliding Implemented. Working on Wall Jumping...
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

<<<<<<< HEAD

            // Manage Hang Time
            if(isGrounded){
                hangCounter = hangTime;
            } else {
                hangCounter -= Time.deltaTime;
            }

            // Manage Jump Buffer
            if(Input.GetButtonDown("Jump") && isGrounded){
                jumpBufferCount = jumpBufferLength;
                jumpTimeCount = jumpTime;
                isJumping = true;
            }
            // Character jump controller
            if(Input.GetButton("Jump") && isJumping){
                if(jumpTimeCount > 0){
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    rb.gravityScale = 0;
                    jumpBufferCount = 0;
                    jumpTimeCount -= Time.deltaTime;
                } else {
                    rb.gravityScale = 5;
                    isJumping = false;
                }
            }

            if((Input.GetButtonUp("Jump") && rb.velocity.y > 0) || jumpTimeCount <= 0){
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                rb.gravityScale = 5;
                isJumping = false;
            }

            if (isTouchingFront && !isGrounded && movement != 0){
                wallSliding = true;
            } else {
                wallSliding = false;
            }

            if(wallSliding){
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));

                if(Input.GetButtonDown("Jump")){
                    wallJumpCounter = wallJumpTime;
                    rb.velocity = new Vector2(-movement * movementSpeed, jumpForce);
                }
            }
=======
        // Set animation boolean for running animation. 
        isRunning = (movement != 0); 
        anim.SetBool("isRunning", isRunning);
>>>>>>> parent of dad1ca8... Dynamic Jumping Added. Dynamic Camera System Added. Hang Time Implemented. Jump Buffer Implemented. Wall Sliding Implemented. Working on Wall Jumping...

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
