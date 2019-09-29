using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMediaPlayerLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject _mediaPlayerPrefab;

    private GameObject _mediaPlayer;

    private Vector3 _positionOffset = Vector3.zero;

    private void Awake()
    {
        if(null == _mediaPlayerPrefab)
        {
            enabled = false;
            return;
        }
    }

    private void OnEnable()
    {
        Debug.Log("MediaPlayerLauncher: On Enable");
        //Vector3 position = GetPosition();
        _mediaPlayer = Instantiate(_mediaPlayerPrefab, transform.position, Quaternion.identity);
        //_mediaPlayer.transform.Rotate(Vector3.up, 90.0f);
    }

    private void OnDisable()
    {
        Debug.Log("MediaPlayerLauncher: On Disable");
        Destroy(_mediaPlayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetPosition()
    {
        return transform.position + transform.TransformDirection(_positionOffset);
    }
}
