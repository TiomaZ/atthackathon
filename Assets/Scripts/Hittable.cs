using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{

    public AdDisplay adDisplay;

    public void hit()
    {
        Debug.Log("I'm hit: " + gameObject.name);
        if(adDisplay != null)
        {
            adDisplay.hit();
        }
    }

    /*
    private void Update()
    {
        Debug.Log("Updating: " + gameObject.name);
    }
    */
}
