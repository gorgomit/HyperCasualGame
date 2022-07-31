using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionScript : MonoBehaviour
{
    public GameObject Player;
    public Text positionText;
    
    private Position position;
    private Text text;

    void Start()
    {
        position = Player.GetComponent<Position>();
    }


    void Update()
    {
        string opponentNumber = position.GetOpponentNumber().ToString();
        positionText.text = position.GetPosition().ToString() + "/" + opponentNumber;
    }
}
