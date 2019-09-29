using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{

    public void hit()
    {
        Debug.Log("I'm hit: " + gameObject.name);
    }

    /*
    private void Update()
    {
        Debug.Log("Updating: " + gameObject.name);
    }
    */
}
