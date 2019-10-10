using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{

    //public AdDisplay adDisplay;

    public void hit()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();

        if (textMesh) {
            string text = textMesh.text;

            Debug.Log("I'm hit: [" + gameObject.name + "][" + text + "]");

            AnalyticsManager.addHit(gameObject.name);
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
