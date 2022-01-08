using UnityEngine;

public class MissionsManager : MonoBehaviour
{
	public static MissionsManager Instance;
	
    [SerializeField] private MissionsDatabase missionsDB;

    [SerializeField] private Transform missionsContainer;
    [SerializeField] private GameObject missionContainerPrefab;
    
    private int currentLevel = 0;
    
    void Awake()
    {
	    Instance = this;
    }
    private void Start()
    {
	    GenerateMissionsUI(currentLevel);
    }
    
    private void GenerateMissionsUI(int level)
    {
		for (int i = 0; i < missionsDB.missionsCount; i++)
		{
			Mission mission = missionsDB.GetMission(i);
			if (mission.level == level)
			{
				MissionContainerUI missionContainer =
					Instantiate(missionContainerPrefab, missionsContainer).GetComponent<MissionContainerUI>();

				missionContainer.gameObject.name = "Mission" + i + "-" + mission.action;
				missionContainer.SetMissionInfo(mission.action + " " + mission.itemName);
				missionContainer.CheckMissionToggle(false);
				mission.missionContainerUI = missionContainer;
			}
		}
	}

    public void CheckMission(ActionType action, string itemName)
    {
	    var mission = missionsDB.IsMission(currentLevel, itemName, action);
	    
	    if (mission == null || mission.missionContainerUI.IsMissionDone())
	    {
		    return;
	    }
	    
	    mission.missionContainerUI.CheckMissionToggle(true);

	    if (missionsDB.AreMissionsDone(currentLevel))
	    {
		    Debug.Log("Missions DONE");
	    }
    }
}