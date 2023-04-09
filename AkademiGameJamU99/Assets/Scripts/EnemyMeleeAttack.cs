using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private Rigidbody2D rb;

    private EnemyScript EnemyScript;
    private float timer;
    public float timeBetweenAttack;
    public int enemyMeleeDamage;
    public Player player;
    public Rigidbody2D playerRb ;
    private Vector2 distanceBetweenEnemyAndPlayer;
    public Vector2 damageDistance;
    public Animator animator;
    public Animator playerAnim;
    
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyScript = GetComponent<EnemyScript>();
    }
    

    void Update()
    {
        if (EnemyScript.GetAlive())
        {
         distanceBetweenEnemyAndPlayer = rb.transform.position - playerRb.transform.position;
                 timer += Time.deltaTime;
                 if (rb.velocity.x == 0 && timer > timeBetweenAttack && EnemyScript.GetDetected() && Mathf.Abs(distanceBetweenEnemyAndPlayer.x) <= damageDistance.x && Mathf.Abs(distanceBetweenEnemyAndPlayer.y) <= 1 )
                 {
                     timer = -1;
                     EnemyAttack();
                     animator.SetBool("isAttacking",true);
                     playerAnim.SetBool("isTakingDamage",true);
                 }
                 else
                 {
                             
                     playerAnim.SetBool("isTakingDamage",false);

                 }
        }
        
    }

    public void EnemyAttack()
    {
        
        player.TakeDamage(enemyMeleeDamage);
    }
}
