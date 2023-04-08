using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    private Rigidbody2D rb;
    public float moveSpeed = 1f;
    public Rigidbody2D playerRb;
    public Vector3 detectDistance;
    private bool detected;
    private bool facingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
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
            direction.y = 0f;
            rb.velocity = moveSpeed * direction;

            if (Mathf.Abs(playerRb.transform.position.x - rb.transform.position.x) < 2)
            {
                moveSpeed = 0;
            }
            else
            {
                moveSpeed = 1f;
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
        }

       
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy Died!");
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }
}
        
    



