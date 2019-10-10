using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{

    static Dictionary<string, int> dictionary = new Dictionary<string, int>();

    public static void addHit(string name)
    {
        int currentCount = 0;

        dictionary.TryGetValue(name, out currentCount);

        dictionary[name] = currentCount++;
    }

    // Start is called before the first frame update
    void Start()
    {
        dictionary = new Dictionary<string, int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
