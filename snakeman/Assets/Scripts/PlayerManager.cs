using System;
using UnityEngine;

[Serializable]
public class PlayerManager{

    public Color m_PlayerColor;
    public Transform m_SpawnPoint;
    public string M_PlayerName;

    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int m_Wins;

    private playerMovement m_Movement;

	public void Setup () {
        m_Wins = 0; //test!!
        m_Movement = m_Instance.GetComponent<playerMovement>();
        m_Movement.playerId = m_PlayerNumber;
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">" + M_PlayerName + "</color>";
    }
	
    public void DisableControl()
    {
        //m_Movement.anim.SetBool("isWalking", false);
        m_Movement.enabled = false;

    }

    public GameObject GetPlayerObject()
    {
        return m_Movement.GetPlayerObject();
    }

    public void EnableControl()
    {
        m_Movement.enabled = true;
    }

    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }
}
