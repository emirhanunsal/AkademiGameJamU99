using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private EnemyScript EnemyScript;
    public float timeBetweenShooting;
    public Player player;
    public int bulletDamage;
    public Animator playerAnim;

    

    private float timer;
    void Start()
    {
        
        EnemyScript = GetComponent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
            timer += Time.deltaTime;
            if (timer > timeBetweenShooting && EnemyScript.GetDetected() && EnemyScript.GetAlive())
            {
                
                timer = 0;
                Shoot();
            }
            
        

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (bullet.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Wall shuriken hit");
            Destroy(gameObject,0.01f);

        }

        if (bullet.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground shuriken hit");
            Destroy(gameObject, 0.01f);
                
        }

        if (bullet.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy bullet hit to player");
            Destroy(gameObject, 0.01f);
            player.TakeDamage(bulletDamage);
        }
        else
        {
            playerAnim.SetBool("isTakingDamage",false);

        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
