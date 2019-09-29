using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeRecognizedBBox : MonoBehaviour
{
    StreamMetadata streamMetadata;
    void Start()
    {
        // Load all json for video once.
        streamMetadata = new StreamMetadata();
        TextAsset targetFile = Resources.Load<TextAsset>("stream_api_hitbox_data_hacked");

        string jsonToParse = targetFile.text;
        Debug.Log(jsonToParse);

        streamMetadata = JsonUtility.FromJson<StreamMetadata>(jsonToParse);
        //streamMetadata = StreamMetadata.CreateFromJSON("{\"values\":" + targetFile.text + "}");
        Debug.Log("got past parsing");


        Debug.Log("Found " + streamMetadata.bboxes.Length + " bboxes");

        //Debug.Log("Found " + streamMetadata.bBoxes[0].id + " is id");
        //Debug.Log("Found " + streamMetadata.bBoxes[0].topLeftOffsetX + " is topleftofx");
        Debug.Log("Found " + streamMetadata.bboxes[0].topLeftOffsetX + " topleftX and topleftY = " + streamMetadata.bboxes[1].topLeftOffsetY);
        
        for(int i = 0; i < 111; i++)
            Debug.Log("time["+i+"]=" + streamMetadata.bboxes[i].time); 
    }

    void Update()
    {
        // check point in time since video playback started
        // map to tenth of second
        float timeSinceVideoStart = Time.time * 10.0f;
        //timeSinceVideoStart = Mathf.Clamp(timeSinceVideoStart, 0.0f, 111.99f);
        int bboxIndex = (int)timeSinceVideoStart;
        bboxIndex %= 111;
        Debug.Log("time is " + Time.time + " bboxIndex = " + bboxIndex);
        if (streamMetadata.bboxes[bboxIndex].type != "")
        {
            // Draw bounding box
            gameObject.GetComponent<Transform>().localScale = new Vector3(streamMetadata.bboxes[bboxIndex].width / 100.0f, streamMetadata.bboxes[bboxIndex].height / 100.0f, 0.0f);
            gameObject.GetComponent<Transform>().localPosition = new Vector3((streamMetadata.bboxes[bboxIndex].topLeftOffsetX - 50) / 100.0f + streamMetadata.bboxes[bboxIndex].width / 200.0f, (50.0f - streamMetadata.bboxes[bboxIndex].topLeftOffsetY) / 100.0f + streamMetadata.bboxes[bboxIndex].height / 200.0f, -0.01f);
        }
        else
        {
            gameObject.GetComponent<Transform>().localScale = new Vector3(0.0f, 0.0f, 0.0f);
            gameObject.GetComponent<Transform>().localPosition = new Vector3(0.0f, 0.0f, -0.01f);
        }
        // look up in array of "mlframe" data
    }
}
