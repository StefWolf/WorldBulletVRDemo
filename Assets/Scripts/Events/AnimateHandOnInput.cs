using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class AnimateHandOnInput : MonoBehaviour
{
    
    public InputActionProperty inputActionProperty;
    ActionBasedController actionBased;
    public InputAction action;

    void Start()
    {
        
    }

    
    void Update()
    {
        float triggerValue = inputActionProperty.action.ReadValue<float>();
        //float a = actionBased.ReadValue(action);
       
    }
}
