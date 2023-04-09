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
    private bool facingRight = true;
    public Animator animator;


    [SerializeField] private TrailRenderer tr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);

        }
        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            Flip();
        }

        else if (Input.GetAxis("Horizontal") <0 && facingRight)
        {
            Flip();
        }

      
        //----------MOVEMENT
        rb.transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * Input.GetAxis("Horizontal"),
            transform.position.y);


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning",true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


        if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("isAttacking",true);
        }
        else
        {
            animator.SetBool("isAttacking",false);
        }
        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool("isThrowing",true);
        }
        else
        {
            animator.SetBool("isThrowing",false);
        }
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isDashing",true);
        }
        else
        {
            animator.SetBool("isDashing",false);
        }
        //------JUMP
        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            Debug.Log("Jump");
            rb.AddForce(new Vector2(rb.velocity.x,jumpForce),ForceMode2D.Impulse);
            isGrounded = false;
            canDoubleJump = true;
            lastJumpTime = Time.time;
            doubleJumpForce = 20;
            resetAnimBools();

            animator.SetBool("isJumping", true);

        }
        //------------ DOUBLE JUMP
        else if (Input.GetButtonDown("Jump") && canDoubleJump && Time.time > lastJumpTime + doubleJumpCooldown)
        {
            Debug.Log("Double Jump");
            
            rb.AddForce(new Vector2(rb.velocity.x,doubleJumpForce),ForceMode2D.Impulse);
            isGrounded = false;
            canDoubleJump = false;
            
            animator.SetBool("isJumping", true);


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

            tr.emitting = true;
            rb.velocity = new Vector2(dashSpeed * transform.localScale.x * Input.GetAxis("Horizontal"), 0);
            resetAnimBools();

            animator.SetBool("isDashing",true);

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
                        tr.emitting = false;
                        rb.velocity = new Vector2(0, 0);
                        rb.gravityScale = 5;
                    }
                }
            
        
                
    }
        
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDoubleJump = false;
        }
    }
    private void resetAnimBools()
    {
        animator.SetBool("isRunning",false);
        animator.SetBool("isDashing",false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isThrowing", false);
        animator.SetBool("isAttacking",false);
    }
}
    

