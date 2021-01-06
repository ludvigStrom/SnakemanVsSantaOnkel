using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    public void SetPlayerIndex(int playerIndex)
    {
        this.playerIndex = playerIndex;
        titleText.SetText("Player " + (playerIndex + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    public void SetPlayerTeam(string teamName)
    {
        TeamId teamNameId = TeamId.NoId;

        if (teamName == "SantaOnkel")
        {
            teamNameId = TeamId.SantaOnkel;
        }
        else if (teamName == "SnakeMan")
        {
            teamNameId = TeamId.SnakeMan;
        }
        else
        {
            Debug.Log("String did not contain valid team name in enum TeamID");
            return;
        }

        if (!inputEnabled) { return; }

        Debug.Log("Team name" + teamNameId.ToString() + " player index:" + playerIndex);

        PlayerConfigurationManager.Instance.SetPlayerTeam(playerIndex, teamNameId);
        Debug.Log("PM instance set player blalbal done");
        
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }
        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }
}
