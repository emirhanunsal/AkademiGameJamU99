using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject shuriken;
    public Transform shurikenTransform;
    public bool canThrow;
    private float timer;
    public float timeBetweenThrowing;
    
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0,0, rotationZ);

        if (!canThrow)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenThrowing)
            {
                canThrow = true;
                timer = 0;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1) && canThrow)
        {
            canThrow = false;
            Instantiate(shuriken, shurikenTransform.position, quaternion.identity);
        }
    }

    
}
