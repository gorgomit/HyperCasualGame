using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutScript : MonoBehaviour
{

    private Vector3 objectPosition;
    private Vector3 TargetPosition;
    private float BorderX = 2.5f;
    private bool isDown;
    private bool isMoving;

    [Header("Time")]
    public float minTime = 1f;
    public float maxTime = 4f;

    private float time;
    private float moveTime;
    
    public float t;
    private float tempT;


    void Start()
    {
        tempT = t;
        SetRandomTime();
        time = 0;
        objectPosition = transform.position;
        TargetPosition = transform.position;
    }


    void FixedUpdate()
    {
        time += Time.deltaTime;

        if(time >= moveTime)
        {
            if (!isDown && !isMoving)
            {
                TargetPosition.y = TargetPosition.y - BorderX;
                tempT = 1;
                isMoving = true;
            }
            if (objectPosition == TargetPosition && !isDown)
            {
                isDown = true;
                tempT = t;
                TargetPosition.y = TargetPosition.y + BorderX;
            }
        }
        if(isDown && objectPosition == TargetPosition)
        {
            isDown = false;
            time = 0;
            isMoving = false;
        }

        

        objectPosition = transform.position;
        transform.position = Vector3.MoveTowards(objectPosition, TargetPosition, t);
    }

    private void SetRandomTime()
    {
        moveTime = Random.Range(minTime, maxTime);
    }

}
