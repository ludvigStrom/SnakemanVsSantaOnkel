using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameManager gm;
    public int playerNr;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ball"){
            gm.m_Players[playerNr].m_Wins++;
            gm.hasScored = true;
            gm.lastScorer = gm.m_Players[playerNr];
        }
    }
}


