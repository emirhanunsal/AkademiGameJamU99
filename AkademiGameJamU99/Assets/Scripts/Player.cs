using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int enemyBulletDamage = 10;
    public HealthBar healthBar;
    public Animator animator;


    void Start()
    {

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    void Update()
    {animator.SetBool("isDead",false);
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead",true);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(enemyBulletDamage);
        }

    }

}