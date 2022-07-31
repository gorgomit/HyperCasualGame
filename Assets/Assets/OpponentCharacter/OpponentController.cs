using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OpponentController : MonoBehaviour
{

    private Vector3 spawnPoint;
    private Vector3 objectPosition;
    private Vector3 TargetPosition;


    [Header("Settings")]
    public float Speed = 10f;
    public float SwipeSpeed = 10f;
    public float t;

    Animator animator;

    private bool swerve = true;
    private bool move = true;
    private bool isMoving;

    private bool willHit = true;
    private Collider colliderObject;

    private bool onRotatingObject;
    private GameObject RotatingObject;
    private float rotateSpeed;
    private float escapeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spawnPoint = transform.position;
        objectPosition = transform.position;
        TargetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20)
        {
            DeathAndSpawn();
        }
        Vector3 moveVector = new Vector3(0, 0, Speed * Time.deltaTime);
        if (swerve)
        {
            if (!willHit)
            {
                if (!isMoving)
                {
                    TargetPosition.x = Random.Range(-9f, 9f);
                    isMoving = true;
                }
                objectPosition = transform.position;
                TargetPosition.z = objectPosition.z;
                transform.position = Vector3.MoveTowards(objectPosition, TargetPosition, t);
                if(objectPosition == TargetPosition)
                {
                    isMoving = false;
                    willHit = true;
                }
                //moveVector += new Vector3(Time.deltaTime * SwipeSpeed, 0, 0);
            }
        }
        if (onRotatingObject)
        {
            transform.position += new Vector3(-1 * (rotateSpeed - escapeSpeed) / 2 * Time.deltaTime, 0, 0); 
        }
        if (move)
        {
            transform.position += moveVector;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Death")
        {
            if(calculatePosibility(0, 100, 33))
            {
                willHit = true;
            }
            else
            {
                willHit = false;
                colliderObject = other;
            }
        }
        if(other.tag == "RotatingObject")
        {
            RotatingObject = other.gameObject;
            rotateSpeed = RotatingObject.GetComponent<RotatingPlatformScript>().GetRotatingSpeed();
            onRotatingObject = true;
            escapeSpeed = Random.Range(-15f, 15f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RotatingObject")
        {
            onRotatingObject = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            DeathAndSpawn();
        }
        if (collision.gameObject.tag == "Finish")
        {
            swerve = false;
            move = false;
            animator.SetBool("isRunning", false);
        }
        if (collision.gameObject.tag == "Ground")
        {
            move = false;
            animator.SetBool("isRunning", false);
        }
    }

    private void DeathAndSpawn()
    {
        transform.position = spawnPoint;
    }

    private bool calculatePosibility(int min, int max, int limit)
    {
        int posibilty = Random.Range(min, max);
        if(posibilty < limit)
        {
            return true;
        }
        return false;
    }
}
