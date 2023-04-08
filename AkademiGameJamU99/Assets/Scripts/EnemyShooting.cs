using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private EnemyScript EnemyScript;
    public float timeBetweenShooting;
    

    private float timer;
    void Start()
    {
        EnemyScript = GetComponent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
            timer += Time.deltaTime;
            if (timer > timeBetweenShooting && EnemyScript.GetDetected())
            {
                
                timer = 0;
                Shoot();
            }
            
        

    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
