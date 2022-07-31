using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformScript : MonoBehaviour
{

    private GameObject Player;
    public int MoveSpeed;

    private bool inCollider;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player = other.gameObject;
            inCollider = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inCollider = false;
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0,0,MoveSpeed) * Time.deltaTime);
        if (inCollider)
        {
            //Player.transform.position += new Vector3(-1 * MoveSpeed / 2 * Time.deltaTime, 0, 0);
        }
    }
    public float GetRotatingSpeed()
    {
        return MoveSpeed;
    }
}
