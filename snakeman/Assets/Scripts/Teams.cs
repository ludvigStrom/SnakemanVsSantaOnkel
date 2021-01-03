using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamId
{
    NoId = 0,
    SnakeMan = 1,
    SantaOnkel = 2
}

public class Teams : MonoBehaviour
{
    private Team teamOne;
    private Team teamTwo;
    List<Team> teams; 
    private SpawnPointManager spawnPointManager;

    public void Start()
    {
        spawnPointManager = GameObject.Find("SpawnPointManager").GetComponent<SpawnPointManager>();
        
        teamOne = new Team(TeamId.SnakeMan);
        teamTwo = new Team(TeamId.SantaOnkel);

        teams = new List<Team>();
        teams.Add(teamOne);
        teams.Add(teamTwo);
    }

    public void addPlayersToTeams(GameObject[] playersToAdd)
    {
        Debug.Log("Number of teams:" + teams.Count);
        foreach(Team team in teams)
        {
            foreach(GameObject player in playersToAdd)
            {
                Debug.Log("Add player to team " + team.getTeamId());
                PlayerData playerData = player.GetComponent<PlayerData>();
                
                if(playerData.GetTeamId() == team.getTeamId())
                {
                    team.addToTeam(player);
                }
            }
        }
    }

    public Team getTeamById(TeamId id)
    {
        int offsetFromIndex = 1;
        return teams[(int)id - offsetFromIndex];
    }

    public string ShowScore()
    {
        return teamOne.getGoals() + "-" + teamTwo.getGoals();
    }

    public void addGoal(TeamId id)
    {
        if(id == TeamId.SnakeMan)
        {
            teamOne.addGoal();
        }else if(id == TeamId.SantaOnkel)
        {
            teamTwo.addGoal();
        }
        else
        {
            Debug.Log("No score set, check id on goal");
        }
    }

    public int returnMostGoals()
    {
        if(teamOne.getGoals() >= teamTwo.getGoals())
        {
            return teamOne.getGoals();
        }
        else
        {
            return teamTwo.getGoals();
        }
    }

    public TeamId getWinner(int roundsToWin)
    {
        foreach(var team in teams)
        {
            if(team.getGoals() >= roundsToWin)
            {
                return team.getTeamId();
            }
        }
        return TeamId.NoId;
    }


    public List<GameObject> GetPlayers(int i)
    {
        if(i == 1)
        {
            return teamOne.getPlayers();
        }else if(i == 2)
        {
            return teamTwo.getPlayers();
        }
        else
        {
            return null;
        }
    }

    public class Team
    {
        private List<GameObject> players;
        private TeamId teamId;

        private int goals;
    
        public List<GameObject> getPlayers()
        {
            return players;
        }

        public Team(TeamId teamId)
        {
            players = new List<GameObject>();      
            this.teamId = teamId;
            this.goals = 0;
        }

        public void addGoal()
        {
            goals++;
        }

        public TeamId getTeamId()
        {
            return teamId;
        }

        public void addToTeam(List<GameObject> playersToAdd)
        {
            foreach(GameObject playerToAdd in playersToAdd)
            {
                players.Add(playerToAdd);
            }
        }

        public void addToTeam(GameObject playerToAdd)
        {
            players.Add(playerToAdd);
        }

        public int getGoals()
        {
            return goals;
        }
    }
}

