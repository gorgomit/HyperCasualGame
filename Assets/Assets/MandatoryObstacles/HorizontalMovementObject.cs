using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovementObject : MonoBehaviour
{
    private Vector3 objectPosition;
    private Vector3 targetPosition;
    private float BorderX = 8f;

    public float t;
    

    void Start()
    {
        objectPosition = transform.position;
        targetPosition = transform.position;
        targetPosition.x = BorderX;
    }

    void FixedUpdate()
    {

        if(objectPosition == targetPosition)
        {
            if (BorderX == 8f) BorderX = -8f;
            else if(BorderX == -8f) BorderX = 8f;
            targetPosition.x = BorderX;
        }

        objectPosition = transform.position;

        transform.position = Vector3.MoveTowards(objectPosition, targetPosition, t);
    }
}
