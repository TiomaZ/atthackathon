using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{

    Dictionary<string, float> dictionary;

    public void addTime(string name, float timeToAdd)
    {
        float currentTime = 0.0f;

        dictionary.TryGetValue(name, out currentTime);

        dictionary[name] = currentTime + timeToAdd;
    }

    // Start is called before the first frame update
    void Start()
    {
        dictionary = new Dictionary<string, float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
