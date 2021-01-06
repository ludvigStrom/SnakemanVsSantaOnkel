using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField]
    private SpawnPoint[] allSpawnPoints;
    [SerializeField]
    private List<SpawnPoint> teamSnakeman = new List<SpawnPoint>();
    [SerializeField]
    private List<SpawnPoint> teamSantaOnkel = new List<SpawnPoint>();
    
    public void initializeSpawnPoints()
    {
        allSpawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();

        foreach (SpawnPoint spawnPoint in allSpawnPoints)
        {
            if (spawnPoint.getTeamId() == TeamId.SnakeMan)
            {
                teamSnakeman.Add(spawnPoint);
            }
            else if (spawnPoint.getTeamId() == TeamId.SantaOnkel)
            {
                teamSantaOnkel.Add(spawnPoint);
            }
        }
    }

    public List<SpawnPoint> GetSpawnPoints(TeamId teamId)
    {
        if(teamId == TeamId.SnakeMan)
        {
            return teamSnakeman;
        }else if(teamId == TeamId.SantaOnkel)
        {
            return teamSantaOnkel;
        }
        else
        {
            return new List<SpawnPoint>();
        }
    }

    public void assignSpawnPoints(PlayerData[] players)
    {
        int indexSnake = 0;
        int indexSanta = 0;

        foreach(PlayerData player in players)
        {
            if(player.GetTeamId() == TeamId.SnakeMan)
            {
                player.SetSpawnPoint(teamSnakeman[indexSnake].getTransform());
                indexSnake++;
            }else if(player.GetTeamId() == TeamId.SantaOnkel){
                player.SetSpawnPoint(teamSantaOnkel[indexSanta].getTransform());
                indexSanta++;
            }
        }
    }
}
