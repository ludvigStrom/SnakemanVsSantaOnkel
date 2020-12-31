using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public bool hasScored = false;
    public PlayerManager lastScorer;

    public int m_NumRoundsToWin = 3;
    public int startDelay;
    public int endDelay;
    public CameraControl m_CameraControl;
    public Text m_MessageText;
    public GameObject[] playerObjects;
    public PlayerManager[] m_Players;

    public GameObject PrefabSoccerBall;
    public Transform soccerBallSpawnPoint;

    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private PlayerManager m_RoundWinner;
    private PlayerManager m_GameWinner;

    private GameObject currentSoccerBall;

    private playerMovement m_Movement;

    // Use this for initialization
    void Start () {
        lastScorer = null;
        m_StartWait = new WaitForSeconds(startDelay);
        m_EndWait = new WaitForSeconds(endDelay);

        SpawnAllPlayers();
        SetCameraTargets();

        StartCoroutine(GameLoop());
	}

    private void SpawnAllPlayers()
    {
        for(int i = 0; i < m_Players.Length; i++)
        {
            m_Players[i].m_Instance =
                Instantiate(playerObjects[i], m_Players[i].m_SpawnPoint.position, m_Players[i].m_SpawnPoint.rotation) as GameObject;
            m_Players[i].m_PlayerNumber = i + 1;
            m_Players[i].Setup();
        }
    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Players.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Players[i].m_Instance.transform;
        }
        m_CameraControl.m_Targets = targets;
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if(m_GameWinner != null)
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

        //Instantiate 
        currentSoccerBall = Instantiate(PrefabSoccerBall, soccerBallSpawnPoint.position, soccerBallSpawnPoint.rotation) as GameObject;

        ResetAllPlayers();
        DisablePlayerControl();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;

        yield return m_StartWait;
    }

    private void DisablePlayerControl()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            m_Players[i].DisableControl();
        }
    }

    private void EnablePlayerControl()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            m_Players[i].EnableControl();
        }
    }

    private void ResetAllPlayers()
    {
        for(int i = 0; i < m_Players.Length; i++)
        {
            m_Players[i].Reset();
        }
    }

    private IEnumerator RoundPlaying()
    {
        EnablePlayerControl();

        m_MessageText.text = string.Empty;

        while (hasScored == false)
        {
            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        if(lastScorer != null)
        {
            m_RoundWinner = lastScorer;
        }

        DisablePlayerControl();

        m_GameWinner = GetGameWinner();

        GameObject playerwon = m_RoundWinner.GetPlayerObject();

        Animator animator = playerwon.GetComponent<Animator>();
        animator.SetBool("Scored", true);

        string message = EndMessage();
        m_MessageText.text = message;


        yield return m_EndWait;

        animator.SetBool("Scored", false);

        Destroy(currentSoccerBall);
    }

    private string EndMessage()
    {
        string message = "no winner";

        if(m_RoundWinner != null)
        {
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";
        }

        message += "\n\n\n\n";

        for(int i = 0; i < m_Players.Length; i++)
        {
            message += m_Players[i].m_Wins;
            if(i + 1 < m_Players.Length)
            {
                message += "-";
            }
        }

        if (m_GameWinner != null)
        {
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";
        }

        return message;
    }

    private PlayerManager GetGameWinner()
    {
        for(int i = 0; i < m_Players.Length; i++)
        {
            if (m_Players[i].m_Wins == m_NumRoundsToWin)
                return m_Players[i];
        }

        return null;
    }

}