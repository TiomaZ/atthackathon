using MagicLeap;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class FixedImageTrackingVisualizer : MonoBehaviour
{
    #region Private Variables
    private MLImageTrackerBehavior _trackerBehavior = null;
    private bool _targetFound = false;

    [SerializeField, Tooltip("Text to update on ImageTracking changes.")]
    private Text _statusLabel = null;
    // Stores initial text
    private string _prefix;
    private string _eventString;

    [SerializeField, Tooltip("Game Object showing the demo")]
    private GameObject _demo = null;

    private ImageTrackingExample.ViewMode _lastViewMode = ImageTrackingExample.ViewMode.All;
    #endregion

    public Transform trackedImageTransform;

    #region Unity Methods
    /// <summary>
    /// Validate inspector variables
    /// </summary>
    void Awake()
    {
        if (null == _demo)
        {
            Debug.LogError("Error: ImageTrackingVisualizer._demo is not set, disabling script.");
            enabled = false;
            return;
        }
        if (null == _statusLabel)
        {
            Debug.LogError("Error: ImageTrackingVisualizer._statusLabel is not set, disabling script.");
            enabled = false;
            return;
        }
    }

    /// <summary>
    /// Initializes variables and register callbacks
    /// </summary>
    void Start()
    {
        _prefix = _statusLabel.text;
        _statusLabel.text = _prefix + "Target Lost";
        _eventString = "";
        _trackerBehavior = GetComponent<MLImageTrackerBehavior>();
        _trackerBehavior.OnTargetFound += OnTargetFound;
        _trackerBehavior.OnTargetLost += OnTargetLost;
        _trackerBehavior.OnTargetUpdated += OnTargetUpdated;

        RefreshViewMode();
    }

    private void Update()
    {
        _statusLabel.text = String.Format("{0}[{1}/{2}] {3}", _prefix, _trackerBehavior.IsTracking, _trackerBehavior.TrackingStatus, _eventString);
    }

    /// <summary>
    /// Unregister calbacks
    /// </summary>
    void OnDestroy()
    {
        _trackerBehavior.OnTargetFound -= OnTargetFound;
        _trackerBehavior.OnTargetLost -= OnTargetLost;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Update which objects should be visible
    /// </summary>
    /// <param name="viewMode">Contains the mode to view</param>
    public void UpdateViewMode(ImageTrackingExample.ViewMode viewMode)
    {
        _lastViewMode = viewMode;
        RefreshViewMode();
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// De/Activate objects to be hidden/seen
    /// </summary>
    private void RefreshViewMode()
    {
        switch (_lastViewMode)
        {
            case ImageTrackingExample.ViewMode.All:
                _demo.SetActive(_targetFound);
                break;
            case ImageTrackingExample.ViewMode.AxisOnly:
                _demo.SetActive(false);
                break;
            case ImageTrackingExample.ViewMode.TrackingCubeOnly:
                _demo.SetActive(false);
                break;
            case ImageTrackingExample.ViewMode.DemoOnly:
                _demo.SetActive(_targetFound);
                break;
        }
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Callback for when tracked image is found
    /// </summary>
    /// <param name="isReliable"> Contains if image found is reliable </param>
    private void OnTargetFound(bool isReliable)
    {
        //Debug.Log("OnTargetFound");
        _eventString = String.Format("Target Found ({0})", (isReliable ? "Reliable" : "Unreliable"));
        _targetFound = true;
        RefreshViewMode();
    }

    /// <summary>
    /// Callback for when image tracked is lost
    /// </summary>
    private void OnTargetLost()
    {
        //Debug.Log("OnTargetLost");
        _eventString = "Target Lost";
        _targetFound = false;
        RefreshViewMode();
    }

    private void OnTargetUpdated(MLImageTargetResult mlImageTargetResult)
    {
        /*
        //Debug.Log("OnTargetUpdated");
        if(mlImageTargetResult.Status == MLImageTargetTrackingStatus.Tracked && _demo != null && _demo.activeInHierarchy)
        {
            Debug.Log("Setting Position:" + mlImageTargetResult.Position);
            _demo.transform.position = mlImageTargetResult.Position;
            _demo.transform.rotation = mlImageTargetResult.Rotation;
        }
        */
    }

    #endregion
}
