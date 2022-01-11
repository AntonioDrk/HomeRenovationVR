using UnityEngine;

[System.Serializable]
public class Mission
{ 
    public int level;
    public ActionType action;
    public string itemName;
    public Vector3 targetPosition;
    public MissionContainerUI missionContainerUI;
}