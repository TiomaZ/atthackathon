using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{

    //public AdDisplay adDisplay;

    public AnalyticsManager analyticsManager;

    public void hit()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();

        if (textMesh) {
            string text = textMesh.text;

            //Debug.Log("I'm hit: [" + gameObject.name + "][" + text + "]");
            if (analyticsManager != null)
            {
                analyticsManager.addHit(text);
            }
        }

        /*
        if(adDisplay != null)
        {
            adDisplay.hit();
        }
        */
    }

    /*
    private void Update()
    {
        Debug.Log("Updating: " + gameObject.name);
    }
    */
}
