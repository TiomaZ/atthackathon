using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MagicLeapTools;
#if PLATFORM_LUMIN
using UnityEngine.XR.MagicLeap;
#endif

public class ControllerInput : MonoBehaviour
{
    public ControlInput controlInput;

    public GameObject analyticsDisplay;
    public GameObject[] outlines;

    private void Awake()
    {
        controlInput.OnBumperHold.AddListener(HandleBumperHold);
    }

    private void HandleBumperHold()
    {
        Debug.Log("Bumper Hold");
        if(analyticsDisplay)
        {
            analyticsDisplay.SetActive(!analyticsDisplay.activeInHierarchy);
        }

        if(outlines.Length > 0)
        {
           foreach(GameObject outline in outlines) {
                outline.SetActive(!outline.activeInHierarchy);
           }
        }
    }
}
