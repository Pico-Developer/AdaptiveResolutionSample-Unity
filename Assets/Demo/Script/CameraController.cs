using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class CameraController : MonoBehaviour
{
    [SerializeField] private List<Camera> camList;
    [SerializeField] private List<GameObject> offsetList;
    
    List<InputDevice> leftDevices, rightDevices;
    bool inputValue;
    int mode, prevmode;
    InputDeviceCharacteristics rightTrackedControllerFilter;
    InputDeviceCharacteristics leftTrackedControllerFilter;

    public bool IsLeftButtonPressed { get; private set; }
    public bool IsRightButtonPressed { get; private set; }


    void Start()
    {
        leftDevices = new List<InputDevice>();
        rightDevices = new List<InputDevice>();

        mode = 0;

        IsLeftButtonPressed = false;
        IsRightButtonPressed = false;

        camList[0].enabled = true;
        camList[1].enabled = false;
        camList[2].enabled = false;

        rightTrackedControllerFilter = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Right;
        leftTrackedControllerFilter = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left; 
    }

    void CameraSelect(int mode, int prevmode)
    {
        mode %= 3;
        prevmode %= 3;
        offsetList[mode].transform.localPosition = Quaternion.Inverse(camList[prevmode].transform.localRotation) * new Vector3(-camList[prevmode].transform.localPosition.x, 0, -camList[prevmode].transform.localPosition.z);
        offsetList[mode].transform.localRotation = Quaternion.Inverse(camList[prevmode].transform.localRotation.ConstrainYaw());
        camList[mode].enabled = true;
        camList[(mode + 1) % 3].enabled = false;
        camList[(mode + 2) % 3].enabled = false;
    }

    void Update()
    {
        //https://docs.unity3d.com/ScriptReference/XR.InputDevices.GetDevicesWithCharacteristics.html
        InputDevices.GetDevicesWithCharacteristics(rightTrackedControllerFilter, rightDevices);
        InputDevices.GetDevicesWithCharacteristics(leftTrackedControllerFilter, leftDevices);
        prevmode = mode;
        foreach (InputDevice device in rightDevices)
        {
            //https://developer-global.pico-interactive.com/document/unity/input-mapping/
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out inputValue) && inputValue ||
                device.TryGetFeatureValue(CommonUsages.primaryButton, out inputValue) && inputValue ||
                device.TryGetFeatureValue(CommonUsages.triggerButton, out inputValue) && inputValue)
            { 
                if (!IsRightButtonPressed)
                {
                    IsRightButtonPressed = true;
                    mode++;
                }
            }
            else
            {
                IsRightButtonPressed = false;
            }
        }
        foreach (InputDevice device in leftDevices)
        {
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out inputValue) && inputValue ||
             device.TryGetFeatureValue(CommonUsages.primaryButton, out inputValue) && inputValue ||
             device.TryGetFeatureValue(CommonUsages.triggerButton, out inputValue) && inputValue)
            {
                if (!IsLeftButtonPressed)
                {
                    IsLeftButtonPressed = true;
                    mode += 2;
                }
            }
            else
            {
                IsLeftButtonPressed = false;
            }
        }
        if (mode != prevmode)
        {
            CameraSelect(mode, prevmode);
        }
    }
}