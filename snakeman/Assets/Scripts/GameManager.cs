using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public bool hasScored = false;

    public int roundsToWin = 3;
    public int startDelay;
    public int endDelay;
    private CameraControl cameraControl;
    public Text messageText;
 
    public GameObject PrefabSoccerBall;
    public Transform soccerBallSpawnPoint;

    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;

    private TeamId roundWinner;
    private TeamId gameWinner;
    public TeamId lastScorer;

    private GameObject currentSoccerBall;

    private playerMovement movement;

    public GameObject[] players;
    private Teams teams;

    private SpawnPointManager spawnPointManager;

    void Start () {
        roundWinner = TeamId.NoId;

        cameraControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();

        spawnPointManager = GameObject.Find("SpawnPointManager").GetComponent<SpawnPointManager>();
        spawnPointManager.initializeSpawnPoints();

        lastScorer = TeamId.NoId;
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        teams = GameObject.FindGameObjectWithTag("Teams").GetComponent<Teams>();

        SetupAllPlayers();
        SetCameraTargets();

        StartCoroutine(GameLoop());
	}

    private void SetupAllPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        teams.addPlayersToTeams(players);

        PlayerData[] allPlayersData = FindObjectsOfType<PlayerData>();
        spawnPointManager.assignSpawnPoints(allPlayersData);        

        foreach (PlayerData player in allPlayersData)
        {
            player.ResetToSpawn();
        }
    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[players.Length];

        int i = 0;
        foreach(GameObject player in players)
        {
            targets[i] = player.gameObject.transform;
            i++;
        }

        cameraControl.m_Targets = targets;
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if(teams.returnMostGoals() >= roundsToWin)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        hasScored = false;

        currentSoccerBall = Instantiate(PrefabSoccerBall, soccerBallSpawnPoint.position, soccerBallSpawnPoint.rotation) as GameObject;

        ResetAllPlayers();
        DisablePlayerControl();

        cameraControl.SetStartPositionAndSize();

        roundNumber++;
        messageText.text = "ROUND " + roundNumber;

        yield return startWait;
    }

    private void DisablePlayerControl()
    {
        foreach (GameObject player in players)
        {
            PlayerData playerData = player.gameObject.GetComponent<PlayerData>();
            playerData.DisableControl();
        }
    }

    private void EnablePlayerControl()
    {
        foreach (GameObject player in players)
        {
            PlayerData playerData = player.gameObject.GetComponent<PlayerData>();
            playerData.EnableControl();
        }
    }

    private void ResetAllPlayers()
    {
        foreach(GameObject player in players)
        {
            PlayerData playerData = player.gameObject.GetComponent<PlayerData>();
            playerData.ResetPlayer();
        }
    }

    private IEnumerator RoundPlaying()
    {
        EnablePlayerControl();

        messageText.text = string.Empty;

        while (hasScored == false)
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        
        if(lastScorer != TeamId.NoId)
        {
            roundWinner = lastScorer;
        }

        DisablePlayerControl();

        gameWinner = GetGameWinner();

        TeamId playerwon = roundWinner;

        /*TODO: Animation of socrer
        Animator animator = playerwon.GetComponent<Animator>();
        animator.SetBool("Scored", true);
        */

        string message = EndMessage();
        messageText.text = message;

        //animator.SetBool("Scored", false);

        yield return endWait;
        Destroy(currentSoccerBall);
    }

    private TeamId GetGameWinner()
    {
        return teams.getWinner(roundsToWin);
    }

    private string EndMessage()
    {
        Debug.Log("Last scorer is: " + lastScorer);
        Debug.Log("Round winner is: " + roundWinner);
        Debug.Log("Score is: " + teams.ShowScore());

        String message = "";

        if(lastScorer != TeamId.NoId)
        {
            message = lastScorer + " wins the round!";
        }

        message += "\n\n\n\n" + teams.ShowScore();

        if (gameWinner != TeamId.NoId)
        {
            message = gameWinner + " wins the game!";
        }
        
        return message;
    }
}