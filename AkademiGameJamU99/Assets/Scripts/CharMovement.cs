using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    public float jumpForce = 20f;
    public float doubleJumpForce = 10f;
    public bool isGrounded;
    public bool canDoubleJump;
    public float dashSpeed = 15f;
    public float dashTime = 0.25f;
    public float cooldown = 0.5f;
    private bool isDashing = false;
    private float dashTimeLeft = 0f;
    private float lastDashTime = -5f;
    private float lastJumpTime;
    public float doubleJumpCooldown = 0.15f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //----------MOVEMENT
        rb.transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * Input.GetAxis("Horizontal"),
            transform.position.y);
        
        
        //------JUMP
        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            Debug.Log("Jump");
            rb.AddForce(new Vector2(rb.velocity.x,jumpForce),ForceMode2D.Impulse);
            isGrounded = false;
            canDoubleJump = true;
            lastJumpTime = Time.time;
            doubleJumpForce = 20;
        }
        //------------ DOUBLE JUMP
        else if (Input.GetButtonDown("Jump") && canDoubleJump && Time.time > lastJumpTime + doubleJumpCooldown)
        {
            Debug.Log("Double Jump");
            
            rb.AddForce(new Vector2(rb.velocity.x,doubleJumpForce),ForceMode2D.Impulse);
            isGrounded = false;
            canDoubleJump = false;

        }
        //-------------DASH
        if (Input.GetKeyDown(KeyCode.LeftControl) && Time.time > cooldown + lastDashTime)
        {
            isDashing = true;
            dashTimeLeft = dashTime;
            lastDashTime = Time.time;
            if (!isGrounded)
            {
                rb.gravityScale = 0;
            }
            rb.velocity = new Vector2(dashSpeed * transform.localScale.x * Input.GetAxis("Horizontal"), 0);
            
        }
        //------ DASH CONTROL
        if (isDashing)
            doubleJumpForce = 20;
                if (dashTimeLeft > 0)
                {
                    dashTimeLeft -= Time.deltaTime;
                    if (dashTimeLeft <= 0)
                    {
                        isDashing = false;
                        rb.velocity = new Vector2(0, 0);
                        rb.gravityScale = 5;
                    }
                }
            
        
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDoubleJump = false;
        }
    }

    
}
