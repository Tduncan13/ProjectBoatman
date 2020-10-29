using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    Animator anim;
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public bool isRunning = false;
    private bool turn = false;
    private bool facingRight = true;
    private float posX;
    private float scaleX;

    private Rigidbody2D _rigidBody;

    private void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
        posX = transform.position.x;
        scaleX = transform.localScale.x;
        anim = GetComponent<Animator>();
    }

    private void Update() {
        
        // Horizontal character movement controller.
        var movement = Input.GetAxisRaw("Horizontal") * Time.deltaTime * MovementSpeed;
        transform.position += new Vector3(movement, 0, 0);

        // Set animation boolean for running animation. 
        isRunning = (movement != 0); 
        anim.SetBool("isRunning", isRunning);

        // Check character movement to get character orientation. 
        turn = (movement < 0 && facingRight) ^ (movement > 0 && !facingRight);
        if(turn){
            scaleX = -scaleX;
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
            facingRight = !facingRight;
        } 

        // Character jump controller
        if(Input.GetButtonDown("Jump") && Mathf.Abs(_rigidBody.velocity.y) < 0.001f){
            _rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        // Get new character position. 
        posX = transform.position.x;
    }
}
