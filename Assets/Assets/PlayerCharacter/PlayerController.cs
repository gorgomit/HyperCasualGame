using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Reference")]
    private Vector3 spawnPoint;
    [Header("Settings")]
    public float Speed = 10f;
    public float SwipeSpeed = 10f;

    private float firstTouchX;
    private float rotateSpeed;
    Animator animator;

    public bool swerve = true;
    private bool move = true;

    [SerializeField] private GameObject _SplatPrefab;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < -20)
        {
            DeathAndSpawn();
        }
        Vector3 moveVector = new Vector3(-1 * rotateSpeed * Time.deltaTime, 0, Speed * Time.deltaTime);
        if (swerve)
        {
            float diff = 0;

            if (Input.GetMouseButtonDown(0))
            {
                firstTouchX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                float lastTouchX = Input.mousePosition.x;
                diff = lastTouchX - firstTouchX;
                moveVector += new Vector3(diff * Time.deltaTime * SwipeSpeed, 0, 0);
                firstTouchX = lastTouchX;
            }
            
        }
        if (move)
        {
            transform.position += moveVector;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            DeathAndSpawn();
        }
        if(collision.gameObject.tag == "Finish")
        {
            swerve = false;
        }
        if(collision.gameObject.tag == "Ground")
        {
            
            move = false;
            animator.SetBool("isRunning", false);
            GameObject obj = Instantiate(_SplatPrefab, transform.position, _SplatPrefab.transform.rotation);
        }
        if(collision.gameObject.tag == "RotatingObject")
        {
            rotateSpeed = collision.gameObject.GetComponent<RotatingPlatformScript>().GetRotatingSpeed();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "RotatingObject")
            rotateSpeed = 0;
    }

    private void DeathAndSpawn()
    {
        transform.position = spawnPoint;
    }
}
