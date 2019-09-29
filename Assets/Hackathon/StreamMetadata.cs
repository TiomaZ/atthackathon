using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StreamMetadata
{
    public BBox[] bboxes;

/*
    public static StreamMetadata CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<StreamMetadata>(jsonString);
    }
    */
}
