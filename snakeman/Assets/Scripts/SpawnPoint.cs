using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public TeamId teamId;
    
    public Transform getTransform()
    {
        return this.gameObject.transform;
    }

    public TeamId getTeamId()
    {
        return teamId;
    }
}
