using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
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
    public float movementSpeed = 1, jumpForce = 1, checkRadius, jumpBufferLength;
    private float jumpBufferCount;
    public bool isRunning, isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;


    // Wall Jump/Slide Variables
    public Transform frontCheck;
    public float wallSlideSpeed, wallJumpForceX, wallJumpForceY, wallJumpTime, wallJumpCounter;
    public bool isTouchingFront, wallJumping, wallSliding;


    // Hang time Variables
    public float hangTime = 0.2f;
    private float hangCounter;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() {

        if (wallJumpCounter <= 0) {
            // Horizontal character movement controller.
            var movement = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(movement * movementSpeed, rb.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

            // Set animation boolean for running animation. 
            isRunning = (movement != 0); 

            // Check character movement to get character orientation. 
            if(movement > 0){
                spriteRenderer.flipX = false;
            } else if (movement < 0){
                spriteRenderer.flipX = true;
            } 

            // Move Camera Point
            if(movement != 0){
                cameraTarget.localPosition = new Vector3(
                    Mathf.Lerp(cameraTarget.localPosition.x, aheadAmount * movement, aheadSpeed * Time.deltaTime),
                    cameraTarget.localPosition.y, 
                    cameraTarget.localPosition.z);

                Debug.Log(cameraTarget.localPosition);
            }

            // Manage Hang Time
            if(isGrounded){
                hangCounter = hangTime;
            } else {
                hangCounter -= Time.deltaTime;
            }

            // Manage Jump Buffer
            if(Input.GetButtonDown("Jump")){
                jumpBufferCount = jumpBufferLength;
            } else {
                jumpBufferCount -= Time.deltaTime;
            }

            // Character jump controller
            if(jumpBufferCount >= 0 && hangCounter > 0){
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                jumpBufferCount = 0;
            }

            if(Input.GetButtonUp("Jump") && rb.velocity.y > 0){
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
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

            if(Input.GetButtonDown("Jump") && wallSliding){
                wallJumping = true;
                Invoke("SetWallJumpingToFalse", wallJumpTime);
            }

            if (wallJumping){
                rb.velocity = new Vector2(wallJumpForceX * -movement, wallJumpForceY);
            }

        } else {
            wallJumpCounter -= Time.deltaTime;
        }

        anim.SetBool("isRunning", isRunning);
    }

    void flip(){
        //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void SetWallJumpingToFalse(){
        wallJumping = false;
    }
}
