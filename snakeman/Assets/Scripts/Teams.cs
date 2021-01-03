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
    List<Team> teams = new List<Team>();

    [SerializeField]
    private SpawnPointManager spawnPointManager;

    public void Start()
    {
        spawnPointManager = GameObject.Find("SpawnPointManager").GetComponent<SpawnPointManager>();
        
        Team teamOne = new Team(TeamId.SnakeMan, spawnPointManager.GetSpawnPoints(TeamId.SnakeMan));
        Team teamTwo = new Team(TeamId.SantaOnkel, spawnPointManager.GetSpawnPoints(TeamId.SantaOnkel));
        teams.Add(teamOne);
        teams.Add(teamTwo);
    }

    public void addPlayersToTeams(GameObject[] playersToAdd)
    {
        foreach(Team team in teams)
        {
            foreach(GameObject player in playersToAdd)
            {
                PlayerData playerData = player.GetComponent<PlayerData>();
                
                if(playerData.GetTeamId() == team.getTeamId())
                {
                    teamOne.addToTeam(player);
                }
            }
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

        public List<SpawnPoint> spawnPoints;

        private int goals;
    
        public List<GameObject> getPlayers()
        {
            return players;
        }

        public Team(TeamId teamId, List<SpawnPoint> spawnPoints)
        {
            players = new List<GameObject>();
            this.spawnPoints = spawnPoints;            
            this.teamId = teamId;
            this.goals = 0;
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

