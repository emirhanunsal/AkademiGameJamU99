using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class EnemyScript : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    private Rigidbody2D rb;
    private float moveSpeed;
    public float enemyMoveSpeed;
    public Rigidbody2D playerRb;
    public Vector3 detectDistance;
    private bool detected;
    private bool facingRight;
    private bool alive = true;
    public bool isFlying;
    public int stopRange;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        
        animator.Play("idle");

    }

    void Update()
    {
        if (alive)
        {
            if (Mathf.Abs(rb.transform.position.x - playerRb.transform.position.x) < detectDistance.x &&
                Mathf.Abs(rb.transform.position.y - playerRb.transform.position.y) < detectDistance.y)
            {
                Debug.Log("Detected");
                detected = true;
            }

            if (detected)
            {
                if (playerRb.transform.position.x - rb.transform.position.x < 0 && !facingRight)
                {
                    Flip();
                }

                if (playerRb.transform.position.x - rb.transform.position.x > 0 && facingRight)
                {
                    Flip();
                }

                Vector2 direction = (playerRb.transform.position - rb.transform.position);
                if (!isFlying)
                {
                    direction.y = 0f;
                }
                
                rb.velocity = moveSpeed * direction;
                animator.SetBool("isRunning",true);
               
                if (Mathf.Abs(playerRb.transform.position.x - rb.transform.position.x) <= stopRange)
                {
                    moveSpeed = 0f;
                    animator.Play("Attack");
                    animator.SetBool("isRunning",false);
                }
                else
                { animator.SetBool("isAttacking",false);
                    moveSpeed = 1f;
                }

            }


        }
        
    }

   
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    public bool GetDetected()
    {
        return detected;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Shuriken"))
        {
            detected = true;
            TakeDamage(50);
            Debug.Log("Shuriken hit damage");
        }

       
    }
    
    public bool GetAlive()
    {
        return alive;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.Play("Hurt");

        if (currentHealth <= 0)
        {
            Die();
           animator.SetBool("isDeadEnemy",true);
        }
    }
    void Die()
    {
        Debug.Log("Enemy Died!");
        rb.gravityScale = 1;
        animator.Play("Dead");
        
        this.enabled = false; 
        alive = false;
        Destroy(rb,3);
        
    }
}
        
    



