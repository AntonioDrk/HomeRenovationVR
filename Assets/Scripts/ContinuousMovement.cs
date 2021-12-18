using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
public class ContinuousMovement : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    [SerializeField] private XROrigin rig;
    
    
    public float fallingSpeed = -10f;
    public XRController lcontroller;
    public XRNode inputSource;
    private Vector2 inputAxis;
    
    public float speed = 1;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        //Debug.Log(inputAxis);
    }

    private void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 dir = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(dir * speed * Time.fixedDeltaTime);
        
        // Gravity
        //character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }
}
