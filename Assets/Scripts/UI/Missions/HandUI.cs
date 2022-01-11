using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    [SerializeField] private Transform missionContainer;

    private void OnEnable()
    {
        if (MissionsManager.Instance != null)
        {
            MissionsManager.Instance.AddMissionContainer(missionContainer);
        }
        else
        {
            Debug.LogError("No Mission Manager in Scene!");
        }
        
    }
}
