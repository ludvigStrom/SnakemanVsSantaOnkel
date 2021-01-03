using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameManager gameManager;
    public int playerNr;

    public TeamId teamId;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if(other.gameObject.tag == "Ball"){
            gm.players[playerNr].m_Wins++;
            gm.hasScored = true;
            gm.lastScorer = gm.players[playerNr];
        }*/
    }
}


