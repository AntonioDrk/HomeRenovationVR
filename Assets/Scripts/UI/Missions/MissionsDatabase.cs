using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu (fileName = "MissionsDatabase", menuName = "Create Database/Missions database")]
public class MissionsDatabase : ScriptableObject
{
    [SerializeField] private List<Mission> missions;

    public int missionsCount => missions.Count;

    public Mission GetMission(int id)
    {
        return missions[id];
    }

    public Mission IsMission(int level, string itemName, ActionType action)
    {
        return missions.FirstOrDefault(mission => mission.level == level && mission.itemName == itemName && mission.action == action);
    }

    public bool AreMissionsDone(int level)
    {
        return missions.All(mission => mission.level != level || mission.missionContainerUI.IsMissionDone());
    }
    
}