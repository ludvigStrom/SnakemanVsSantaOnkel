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

    private PlayerData roundWinner;
    private TeamId gameWinner;

    private GameObject currentSoccerBall;

    private playerMovement movement;

    private PlayerData lastScorer;
    public GameObject[] players;
    private Teams teams;

    private SpawnPointManager spawnPointManager;

    void Start () {
        cameraControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();

        spawnPointManager = GameObject.Find("SpawnPointManager").GetComponent<SpawnPointManager>();
        spawnPointManager.initializeSpawnPoints();

        lastScorer = null;
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
        PlayerData[] allPlayersData = FindObjectsOfType<PlayerData>();

        spawnPointManager.assignSpawnPoints(allPlayersData);

        foreach (PlayerData player in allPlayersData)
        {
            player.SpawnPlayer();
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

        if(returnMostGoals() >= roundsToWin)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private int returnMostGoals()
    {
        return teams.returnMostGoals();
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
        
        if(lastScorer != null)
        {
            roundWinner = lastScorer;
        }

        DisablePlayerControl();

        gameWinner = GetGameWinner();

        GameObject playerwon = roundWinner.GetPlayerObject();

        Animator animator = playerwon.GetComponent<Animator>();

        animator.SetBool("Scored", true);

        string message = EndMessage();
        messageText.text = message;

        animator.SetBool("Scored", false);

        yield return endWait;
        Destroy(currentSoccerBall);
    }

    private TeamId GetGameWinner()
    {
        return teams.getWinner(roundsToWin);
    }

    private string EndMessage()
    {
        /*
        if(roundWinner != null)
        {
            message = roundWinner.m_ColoredPlayerText + " WINS THE ROUND!";
        }

        message += "\n\n\n\n";

        for(int i = 0; i < players.Length; i++)
        {
            message += players[i].m_Wins;
            if(i + 1 < players.Length)
            {
                message += "-";
            }
        }

        if (gameWinner != null)
        {
            message = gameWinner.m_ColoredPlayerText + " WINS THE GAME!";
        }else{
            string message = "no winner";
        }
        

        return message;
        */
        return null;
    }

}