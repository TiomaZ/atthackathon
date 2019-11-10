using UnityEngine;

public class VisualizeRecognizedBBox : MonoBehaviour
{
    StreamMetadata streamMetadata;
    BBoxes bBoxes;
    Transform[] bboxTransform;
    int lastFrameIndex = 0;
    float mLastUpdateUITime = 0.0f;
    float timeSinceVideoStart = 0.0f;
    float timeSinceSecondStart = 0.0f;
        
    MagicLeap.MediaPlayerExample mediaPlayerExample;
    void Start()
    {
        mediaPlayerExample = GameObject.Find("MediaPlayerExample").GetComponent<MagicLeap.MediaPlayerExample>();
        bboxTransform = new Transform[4];
        for (int k = 0; k < 4; k++)
        {
            bboxTransform[k]= gameObject.transform.GetChild(k).transform;
            //Debug.Log("added " + bboxTransform[k].gameObject.name);
        }
        // Load all json for video once.
        streamMetadata = new StreamMetadata();
        bBoxes = new BBoxes();
        TextAsset targetFile = Resources.Load<TextAsset>("video_1");

        string jsonToParse = targetFile.text;
        //Debug.Log(jsonToParse);

        streamMetadata = JsonUtility.FromJson<StreamMetadata>(jsonToParse);
        //streamMetadata = StreamMetadata.CreateFromJSON("{\"values\":" + targetFile.text + "}");
        //Debug.Log("got past parsing streamMetadata: " + jsonToParse);
        //Debug.Log("Found " + streamMetadata.mlFrames.Length + " mlFrames");
        //Debug.Log("Found " + streamMetadata.bBoxes[0].id + " is id");
        //Debug.Log("Found " + streamMetadata.bBoxes[0].topLeftOffsetX + " is topleftofx");
        //Debug.Log("Found mlData to parse later " + streamMetadata.mlFrames[0].mlData);
        
        //for(int i = 0; i < 264; i++)
            //Debug.Log("time["+i+"]=" + streamMetadata.mlFrames[i].time); 
    }

    void Update()
    {
        // Track time since video playback started to the tenth of a second, as that is how often machine-learned object-identification .json has data.
        //Debug.Log("timeSinceStart: " + Time.time + " elapsedTime: " + mediaPlayerExample.GetElapsedTimeMs() + " updateUITime: " + mediaPlayerExample.GetUiUpdateTimeMs());
        int currentWholeSecondsOfElapsedTime = mediaPlayerExample.GetElapsedTimeMs() / 1000;
        if ((int)timeSinceVideoStart != (currentWholeSecondsOfElapsedTime))
        {
            timeSinceSecondStart = 0.0f;
        }
        timeSinceVideoStart = mediaPlayerExample.GetElapsedTimeMs() / 1000.0f;
        if (mLastUpdateUITime > mediaPlayerExample.GetUiUpdateTimeMs())
        {
            // a tenth of a second has passed, and the frame index needs updated...
            timeSinceSecondStart += 0.1f;
        }
        mLastUpdateUITime = mediaPlayerExample.GetUiUpdateTimeMs();
        
        int frameIndex = (int)(10 * (timeSinceVideoStart + timeSinceSecondStart));
        frameIndex %= 264;
        //frameIndex = 263;
        //Debug.Log("video time is " + (timeSinceVideoStart + timeSinceSecondStart) + " frameIndex = " + frameIndex);

        // frame index only gets updated every .1 seconds, so not every frame
        if (frameIndex != lastFrameIndex)
        {
            lastFrameIndex = frameIndex;
            // clear last frame of bounding box data...
            for (int k = 0; k < 4; k++)
            {
                transform.localPosition = Vector3.zero;
                transform.localScale = Vector3.one;
                bboxTransform[k].localScale = Vector3.zero;
                bboxTransform[k].localPosition = Vector3.zero;
            }

            //Debug.Log("mlFrames.Length="+streamMetadata.mlFrames.Length);
            if (streamMetadata.mlFrames[frameIndex].mlData != "[]")
            {
                // Need to parse the array of bbox entries:
                string jsonToParseBBox = "{\"bboxes\":" + streamMetadata.mlFrames[frameIndex].mlData + "}";
                //Debug.Log("mlData was found to be " + jsonToParseBBox);
                bBoxes = JsonUtility.FromJson<BBoxes>(jsonToParseBBox);
                // Draw bounding box
                //Debug.Log(bBoxes.bboxes.Length + " boxes this frame");
                for (int j = 0; j < bBoxes.bboxes.Length; j++)
                {
                    float normalizedBoxWidth = bBoxes.bboxes[j].bbox.width / 100.0f;
                    float normalizedBoxHeight = bBoxes.bboxes[j].bbox.height / 100.0f;
                    //Debug.Log(bBoxes.bboxes[j].bbox.height + " w:" + bBoxes.bboxes[j].bbox.width + " x:" + bBoxes.bboxes[j].bbox.topLeftOffsetX + " y:" + bBoxes.bboxes[j].bbox.topLeftOffsetY);
                    bboxTransform[j].localScale = new Vector3(
                        normalizedBoxWidth,
                        normalizedBoxHeight,
                        0.0f);
                    bboxTransform[j].localPosition = new Vector3(
                        -0.5f + bBoxes.bboxes[j].bbox.topLeftOffsetX / 100.0f + normalizedBoxWidth / 2.0f,
                        0.5f - bBoxes.bboxes[j].bbox.topLeftOffsetY / 100.0f - normalizedBoxHeight / 2.0f,
                        -0.01f);
                        bboxTransform[j].transform.GetChild(0).GetComponent<TextMesh>().text = bBoxes.bboxes[j].bbox.type;
                        bboxTransform[j].transform.GetChild(0).GetComponent<Transform>().localScale = new Vector3(1.0f / normalizedBoxWidth, 1.0f / normalizedBoxHeight, 1.0f);
                }
            }
            else
            {
                gameObject.GetComponent<Transform>().localScale = new Vector3(0.0f, 0.0f, 0.0f);
                gameObject.GetComponent<Transform>().localPosition = new Vector3(0.0f, 0.0f, -0.01f);
            }
            // look up in array of "mlframe" data
        }
    }
}
