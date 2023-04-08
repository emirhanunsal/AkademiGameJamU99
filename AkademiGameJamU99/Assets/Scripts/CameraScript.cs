using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraScript : MonoBehaviour
{

    private Vector3 offSet = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 targetPosition = target.position + offSet;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
    }
}