using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Color playerColor;
    public Transform spawnPoint;
    public string playerName;
    private int goalsMade;

    public int playerId;
     
    public TeamId teamId; 

    [HideInInspector] public string coloredPlayerText;

    private playerMovement movement;

    public void Start()
    {
        goalsMade = 0;
        movement = this.GetComponent<playerMovement>();
        movement.playerId = playerId;
        coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">" + playerName + "</color>";
    }

    public void SetSpawnPoint(Transform sp)
    {
        spawnPoint = sp;
    }

    public void ResetToSpawn()
    {
        this.transform.position = spawnPoint.position;
    }

    public int GetGoalsMade()
    {
        return goalsMade;
    }

    public void ScoreGoalMade()
    {
        goalsMade++;
    }

    public TeamId GetTeamId()
    {
        return teamId;
    }

    public void DisableControl(){
        movement.enabled = false;
    }

    public GameObject GetPlayerObject()
    {
        return movement.GetPlayerObject();
    }

    public void EnableControl()
    {
        movement.enabled = true;
    }

    public void ResetPlayer()
    {
        this.transform.position = spawnPoint.position;
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }
}