using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float lifeTime = 3f;
    public int bulletDamage = 10;
    private Player playerScript;
    private bool takeDamage;
    public Animator animator;
    

    
    void Start()
    {
        playerScript = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        Destroy(gameObject,lifeTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (takeDamage)
        {
            animator.SetBool("isTakingDamage", true);
        }
        else
        {
            animator.SetBool("isTakingDamage", false);

        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Wall shuriken hit");
            Destroy(gameObject,0.01f);

        }

        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground shuriken hit");
            Destroy(gameObject, 0.01f);
                
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy bullet hit to player");
            Destroy(gameObject);
            takeDamage = true;
        }
        

    }

}
