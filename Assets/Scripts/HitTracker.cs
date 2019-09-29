﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class HitTracker : MonoBehaviour
{
    #region Public Variables
    public GameObject Camera;
    public GameObject AdOne;
    #endregion

    #region Private Variables
    private Vector3 _heading;
    private MeshRenderer _meshRenderer;
    #endregion

    private float hitTime;
    private float AdTime;
    private bool AdVisible;

    #region Unity Methods
    void Start()
    {
        MLEyes.Start();
        //transform.position = Camera.transform.position + Camera.transform.forward * 2.0f;
        // Get the meshRenderer component
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();

        hitTime = 0.0f;
        AdTime = 0.0f;
        AdVisible = false;
    }
    private void OnDisable()
    {
        MLEyes.Stop();
    }
    void Update()
    {
        if (MLEyes.IsStarted)
        {
            LayerMask mask = LayerMask.GetMask("HitPlane");

            RaycastHit rayHit;
            _heading = MLEyes.FixationPoint - Camera.transform.position;
            // Use the proper material
            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 10.0f,mask))
            {
                hitTime += Time.deltaTime;
                //Debug.Log("Hit:" + hitTime);
               // Debug.Log("AdVisible: " + AdVisible);
            }
            else
            {
                //Debug.Log("NOT Hit");
                if(hitTime > 0.0f)
                {
                    Debug.Log("Hit Time Reset ("+gameObject.name+"):" + hitTime);
                }
                hitTime = 0.0f;
            }

            if(!AdVisible && hitTime > 5.0f)
            {
                Debug.Log("Ad (" + gameObject.name + "): Turn On");
                AdVisible = true;
                hitTime = 0.0f;
                AdTime = 0.0f;
                if(AdOne != null)
                {
                    AdOne.SetActive(true);
                }
            } else if (AdVisible)
            {
                AdTime += Time.deltaTime;
                //Debug.Log("Ad: add time: " + AdTime);
            }

            if (AdVisible && AdTime > 5.0f)
            {
                Debug.Log("Ad (" + gameObject.name + "): Turn Off");
                AdVisible = false;
                AdTime = 0.0f;
                if(AdOne != null)
                {
                    AdOne.SetActive(false);
                }
            }
        }
    }
    #endregion
}
