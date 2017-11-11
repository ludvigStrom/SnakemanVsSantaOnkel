using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour {

    private GameManager gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager").GetComponent <GameManager> ();
    }

    private void OnCollisionEnter2d(Collision collision)
    {
        Debug.Log("Ball enters goal!");
        if (collision.gameObject.tag == "Player1")
        {
            //give player 2 score;
            gm.m_Players[1].m_Wins++;
        }
        else if(collision.gameObject.tag == "Player2")
        {
            //give player 1 Score;
            gm.m_Players[0].m_Wins++;
        }
        gm.hasScored = true;
    }

}


