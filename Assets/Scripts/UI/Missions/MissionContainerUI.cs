using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissionContainerUI : MonoBehaviour
{
    [SerializeField] private Text missionInfo; 
    [SerializeField] private Toggle missionToggle; 

    public void SetMissionInfo(string txt)
    {
        missionInfo.text = txt;
    }
    
    public void CheckMissionToggle(bool value)
    {
        missionToggle.isOn = value;
    }

    public bool IsMissionDone()
    {
        return missionToggle.isOn;
    }
}