using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Position : MonoBehaviour
{

    public GameObject[] opponents;
    private int numberOfOpponents;
    private float[] distance;
    private bool swerve = true;

    private int currentPosition;
    private int numberOfPlayers;


    // Start is called before the first frame update
    void Start()
    {
        numberOfOpponents = opponents.Length;
        distance = new float[numberOfOpponents];
        numberOfPlayers = numberOfOpponents + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (swerve)
        {
            calculateDistance();
            calculatePosition();
        }
        
    }

    private void calculateDistance()
    {
        for(int i = 0; i < numberOfOpponents; i++)
        {
            distance[i] = transform.position.z - opponents[i].gameObject.transform.position.z;
        }
    }

    private void calculatePosition()
    {
        int positiveNums = 0;
        for(int i = 0; i < numberOfOpponents; i++)
        {
            if(distance[i] > 0)
            {
                positiveNums++;
            }
        }
        currentPosition = numberOfPlayers - positiveNums;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            swerve = false;
        }
    }

    public int GetPosition()
    {
        return currentPosition;
    }

    public int GetOpponentNumber()
    {
        return numberOfOpponents;
    }
}
