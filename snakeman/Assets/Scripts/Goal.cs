using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameManager gameManager;
    private Teams teams;

    [Tooltip("The team who scores if the ball enter the goal")]
    public TeamId GoalId;


    void Start()
    {
        teams = GameObject.FindGameObjectWithTag("Teams").GetComponent<Teams>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            teams.addGoal(GoalId);
            gameManager.lastScorer = GoalId;
            gameManager.hasScored = true;
        }
    }
}


