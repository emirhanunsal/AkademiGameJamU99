using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float lifeTime = 3f;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        Destroy(gameObject,lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
        }
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
            Destroy(gameObject, 0.01f);

        }
    }

}
