using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class FixedRaycastExample : MonoBehaviour
{
    public enum RaycastMode
    {
        Controller,
        Head,
        Eyes
    }

    #region Private Variables
    [SerializeField, Tooltip("The headpose canvas for example status text.")]
    private Text _statusLabel = null;

    [SerializeField, Tooltip("Raycast from eyegaze.")]
    private WorldRaycastEyes _raycastEyes = null;

    [Space, SerializeField, Tooltip("ControllerConnectionHandler reference.")]
    private ControllerConnectionHandler _controllerConnectionHandler = null;

    private RaycastMode _raycastMode = RaycastMode.Controller;
    private int _modeCount = System.Enum.GetNames(typeof(RaycastMode)).Length;

    private float _confidence = 0.0f;
    #endregion

    #region Unity Methods
    /// <summary>
    /// Validate all required components and sets event handlers.
    /// </summary>
    void Awake()
    {
        if (_statusLabel == null)
        {
            Debug.LogError("Error: RaycastExample._statusLabel is not set, disabling script.");
            enabled = false;
            return;
        }

        if (_raycastEyes == null)
        {
            Debug.LogError("Error: RaycastExample._raycastEyes is not set, disabling script.");
            enabled = false;
            return;
        }

        if (_controllerConnectionHandler == null)
        {
            Debug.LogError("Error: RaycastExample._controllerConnectionHandler not set, disabling script.");
            enabled = false;
            return;
        }

        MLInput.OnControllerButtonDown += OnButtonDown;
        UpdateRaycastMode();
    }

    /// <summary>
    /// Cleans up the component.
    /// </summary>
    void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Updates type of raycast and enables correct cursor.
    /// </summary>
    private void UpdateRaycastMode()
    {
        _raycastMode = RaycastMode.Eyes;

        // Default all objects to inactive and then set active to the appropriate ones.
        _raycastEyes.gameObject.SetActive(true);

    }

    /// <summary>
    /// Updates Status Label with latest data.
    /// </summary>
    private void UpdateStatusText()
    {
        _statusLabel.text = string.Format("Raycast Mode: {0}\nRaycast Hit Confidence: {1}", _raycastMode.ToString(), _confidence.ToString());
        if (_raycastMode == RaycastMode.Eyes && MLEyes.IsStarted)
        {
            _statusLabel.text += string.Format("\n\nEye Calibration Status: {0}", MLEyes.CalibrationStatus.ToString());
        }
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Handles the event for button down and cycles the raycast mode.
    /// </summary>
    /// <param name="controllerId">The id of the controller.</param>
    /// <param name="button">The button that is being pressed.</param>
    private void OnButtonDown(byte controllerId, MLInputControllerButton button)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId) && button == MLInputControllerButton.Bumper)
        {
            _raycastMode = (RaycastMode)((int)(_raycastMode + 1) % _modeCount);
            UpdateRaycastMode();
            UpdateStatusText();
        }
    }

    /// <summary>
    /// Callback handler called when raycast has a result.
    /// Updates the confidence value to the new confidence value.
    /// </summary>
    /// <param name="state"> The state of the raycast result.</param>
    /// <param name="result">The hit results (point, normal, distance).</param>
    /// <param name="confidence">Confidence value of hit. 0 no hit, 1 sure hit.</param>
    public void OnRaycastHit(MLWorldRays.MLWorldRaycastResultState state, RaycastHit result, float confidence)
    {
        _confidence = confidence;
        UpdateStatusText();
    }
    #endregion
}
