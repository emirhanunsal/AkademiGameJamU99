using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public float lifeTime = 3f;
    private PlayerCombat playerCombat;
    void Start()
    {
        
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        Destroy(gameObject,lifeTime);
        
        
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

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy shuriken hit");
            
            Destroy(gameObject, 0.01f);
            

        }
    }

    
}
