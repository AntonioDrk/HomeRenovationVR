using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Animator))]
public class HandPresence : MonoBehaviour
{
    public XRNode inputSource;
    [SerializeField] private Animator anim;
    [SerializeField] private CanvasGroup exitBtn;

    void Start()
    {
    }

    
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primBtn))
        {
            if (primBtn)
            {
                exitBtn.alpha = 1f;
                exitBtn.interactable = true;
                exitBtn.blocksRaycasts = true;
            }
            else
            {
                exitBtn.alpha = 0f;
                exitBtn.interactable = false;
                exitBtn.blocksRaycasts = false;
            }
        }
        if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            anim.SetFloat("Trigger", triggerValue);
        }
        else
        {
            anim.SetFloat("Trigger", 0f);
        }

        if (device.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            anim.SetFloat("Grip", gripValue);
        }
        else
        {
            anim.SetFloat("Grip", 0);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
