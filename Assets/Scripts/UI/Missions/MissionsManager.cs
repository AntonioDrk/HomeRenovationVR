using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MissionsManager : MonoBehaviour
{
	public static MissionsManager Instance;
	
    [SerializeField] private MissionsDatabase missionsDB;

    [SerializeField] private Transform missionsContainer;
    [SerializeField] private GameObject missionContainerPrefab;
    
    [SerializeField] private GameObject targetPositionParticles;
    [SerializeField] private float acceptableMoveMissionRange = 1.5f;
    
    private int currentLevel = 0;
    
    void Awake()
    {
	    if (Instance != null)
	    {
		    Destroy(Instance);
	    }
	    Instance = this;
    }
    private void Start()
    {
	    targetPositionParticles.SetActive(false);
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
    
    // checks if mission exists and it's not done yet
    public Mission CheckMission(ActionType action, string itemName)
    {
	    var mission = missionsDB.IsMission(currentLevel, itemName, action);
	    
	    if (mission == null || mission.missionContainerUI.IsMissionDone())
	    {
		    return null;
	    }

	    return mission;
    }

    public void XROnSelectEntered(SelectEnterEventArgs args)
    {
	    SelectedObjectFromMission(args.interactableObject.transform.name);
    }

    public void XROnSelectExited(SelectExitEventArgs args)
    {
	    DroppedObjectFromMission(args.interactableObject.transform.gameObject);
    }

    // pick up object - check if you have a mission to move the current object
    private void SelectedObjectFromMission(string itemName)
    {
	    var mission = CheckMission(ActionType.MOVE, itemName);
	    
	    if (mission == null)
	    {
		    return;
	    }

	    targetPositionParticles.SetActive(true);
	    targetPositionParticles.transform.position = mission.targetPosition;
    }
    
    // drop object - check if you dropped an object from a mission on the target position
    private void DroppedObjectFromMission(GameObject item)
    {
	    var mission = CheckMission(ActionType.MOVE, item.name);
	    
	    if (mission == null)
	    {
		    return;
	    }
		
	    targetPositionParticles.SetActive(false);
	    
	    var distance = Vector3.Distance(item.transform.position, mission.targetPosition);
	    if(distance < acceptableMoveMissionRange)
	    {
		    OnMissionDone(mission);
	    }
    }
    
    public void OnMissionDone(Mission mission)
    {
	    mission.missionContainerUI.CheckMissionToggle(true);

	    if (missionsDB.AreMissionsDone(currentLevel))
	    {
		    Debug.Log("Missions DONE");
	    }
    }

    public void AddMissionContainer(Transform obj)
    {
	    missionsContainer = obj;
	    GenerateMissionsUI(currentLevel);
    }

}